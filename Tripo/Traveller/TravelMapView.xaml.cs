using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Nts.Extensions;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Tiling;
using Mapsui.UI.Maui;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System.Xml;

namespace PleOps.Tripo.Traveller;

public partial class TravelMapView : ContentPage
{
    public TravelMapView()
    {
        InitializeComponent();

        var mapControl = new MapControl();
        mapControl.Map.Layers.Add(OpenStreetMap.CreateTileLayer("dev.pleonex.tripo"));

        var travel = LoadLastTravel();
        if (travel is not null)
        {
            var layer = new MemoryLayer
            {
                Name = "Travel",
                IsMapInfoLayer = true,
                Features = GetPointsFromTravel(travel).ToList(),
                Style = null,
            };
            mapControl.Map.Layers.Add(layer);
            mapControl.Map.Home = n => n.CenterOnAndZoomTo(layer.Extent!.Centroid, n.Resolutions[10]);
        }

        Content = mapControl;
    }

    private Travel? LoadLastTravel()
    {
        var lastTravelFile = Preferences.Default.Get("last_travel_file", string.Empty);
        if (string.IsNullOrEmpty(lastTravelFile))
        {
            return null;
        }

        try
        {
            return TravelFactory.LoadJson(lastTravelFile);
        }
        catch (Exception)
        {
            // do nothing, just show an empty one.
        }

        return null;
    }

    private IEnumerable<IFeature> GetPointsFromTravel(Travel travel)
    {
        IStyle pointStyle = new SymbolStyle()
        {
            SymbolScale = 0.30,
            Fill = new Mapsui.Styles.Brush(Mapsui.Styles.Color.FromString("Red"))
        };
        foreach (Location location in travel.Days.SelectMany(d => d.Activities.Select(a => a.Location)).Where(x => x is not null && x.Coordinates.Longitude != 0))
        {
            var point = SphericalMercator.FromLonLat(location.Coordinates.Longitude, location.Coordinates.Latitude);
            var feature = new PointFeature(point.x, point.y);
            feature.Styles.Add(pointStyle);
            yield return feature;
        }

        var gpxFiles = travel.Days.SelectMany(d => d.Activities).Select(a => a.Transportation.GpxFilePath).Where(x => !string.IsNullOrEmpty(x));
        foreach (var gpxFile in gpxFiles)
        {
            using var xmlReader = XmlReader.Create(gpxFile);
            var gpx = GpxReader.ReadFeatures(xmlReader, new GpxReaderSettings(), NetTopologySuite.Geometries.GeometryFactory.Default);

            foreach (var gpxFeature in gpx.features)
            {
                var lineString = new LineString(gpxFeature.Geometry.Coordinates.Select(v => SphericalMercator.FromLonLat(v.X, v.Y).ToCoordinate()).ToArray());
                var f = new GeometryFeature(lineString);
                f.Styles.Add(new VectorStyle
                {
                    Fill = null,
                    Outline = null,
                    Line = new Pen(Mapsui.Styles.Color.Blue, 4)
                });
                yield return f;
            }
        }
    }
}