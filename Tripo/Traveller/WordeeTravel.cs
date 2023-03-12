namespace PleOps.Tripo.Traveller;

public class WordeeTravel
{
    public int Id { get; set; }
    public string state { get; set; }
    public string mode { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public Descriptiondata descriptionData { get; set; }
    public string descriptionEnd { get; set; }
    public Descriptionenddata descriptionEndData { get; set; }
    public string scope { get; set; }
    public bool enableApproximateDate { get; set; }
    public string dateFrom { get; set; }
    public string dateTo { get; set; }
    public object approximateDateFrom { get; set; }
    public object approximateDateTo { get; set; }
    public Author author { get; set; }
    public object[] users { get; set; }
    public Day[] days { get; set; }
    public int daysCount { get; set; }
    public Direction[] directions { get; set; }
    public Stopplace[] stopPlaces { get; set; }
    public string[] countries { get; set; }
    public object[] videos { get; set; }
    public int countOfPhotos { get; set; }
    public int countOfVideos { get; set; }
    public int countOfComments { get; set; }
    public int countOfLikes { get; set; }
    public bool iLikedThis { get; set; }
    public object introMediaId { get; set; }
    public bool canEdit { get; set; }
    public bool canDelete { get; set; }
    public bool canDefineExactTours { get; set; }
    public object visibility { get; set; }
    public string link { get; set; }
    public Place[] places { get; set; }
    public object[] prepareCheckList { get; set; }
    public Additional additional { get; set; }
    public object verificationState { get; set; }
    public bool isVerified { get; set; }
    public object score { get; set; }
    public object tours { get; set; }
    public object toursEdit { get; set; }
    public Toursinfo toursInfo { get; set; }
    public object authorReviews { get; set; }
}

public class Descriptiondata
{
    public string text { get; set; }
    public object translate { get; set; }
}

public class Descriptionenddata
{
    public string text { get; set; }
    public object translate { get; set; }
}

public class Author
{
    public int id { get; set; }
    public string name { get; set; }
    public bool isPremium { get; set; }
    public string photoPath { get; set; }
    public string cdnPhotoPath { get; set; }
    public string profileUrl { get; set; }
    public string country { get; set; }
}

public class Additional
{
    public string physicalDemands { get; set; }
    public object[] priceList { get; set; }
}

public class Toursinfo
{
    public int countOfActiveTours { get; set; }
    public int countOfApprovedTours { get; set; }
    public bool hasDemandTour { get; set; }
    public object priceFrom { get; set; }
    public object pricePrecision { get; set; }
    public object tours { get; set; }
    public string saleState { get; set; }
}

public class Day
{
    public Stop[] stops { get; set; }
    public Sleepstop sleepStop { get; set; }
    public string sleepType { get; set; }
    public string sleepPlaceId { get; set; }
    public string sleepPlaceResolution { get; set; }
    public string description { get; set; }
    public Descriptiondata2 descriptionData { get; set; }
}

public class Sleepstop
{
    public string description { get; set; }
    public Descriptiondata1 descriptionData { get; set; }
    public object[] stops { get; set; }
    public string placeId { get; set; }
    public string placeResolution { get; set; }
    public object[] media { get; set; }
    public object[] photos { get; set; }
    public object customPoint { get; set; }
    public Additional1 additional { get; set; }
}

public class Descriptiondata1
{
    public string text { get; set; }
    public Translate translate { get; set; }
}

public class Translate
{
    public string sourceLanguage { get; set; }
    public string targetLanguage { get; set; }
    public string text { get; set; }
}

public class Additional1
{
    public bool highlight { get; set; }
    public object parkingPlaceId { get; set; }
    public string physicalDemands { get; set; }
    public object[] priceList { get; set; }
}

public class Descriptiondata2
{
    public string text { get; set; }
    public Translate1 translate { get; set; }
}

public class Translate1
{
    public string sourceLanguage { get; set; }
    public string targetLanguage { get; set; }
    public string text { get; set; }
}

public class Stop
{
    public string type { get; set; }
    public Stop1[] stops { get; set; }
    public string description { get; set; }
    public Descriptiondata3 descriptionData { get; set; }
    public string placeId { get; set; }
    public string placeResolution { get; set; }
    public Medium1[] media { get; set; }
    public Photo1[] photos { get; set; }
    public object customPoint { get; set; }
    public Additional2 additional { get; set; }
}

public class Descriptiondata3
{
    public string text { get; set; }
    public Translate2 translate { get; set; }
}

public class Translate2
{
    public string sourceLanguage { get; set; }
    public string targetLanguage { get; set; }
    public string text { get; set; }
}

public class Additional2
{
    public bool highlight { get; set; }
    public string parkingPlaceId { get; set; }
    public string physicalDemands { get; set; }
    public Pricelist[] priceList { get; set; }
}

public class Pricelist
{
    public string name { get; set; }
    public string price { get; set; }
    public string currency { get; set; }
}

public class Stop1
{
    public string description { get; set; }
    public Descriptiondata4 descriptionData { get; set; }
    public object[] stops { get; set; }
    public string placeId { get; set; }
    public string placeResolution { get; set; }
    public Medium[] media { get; set; }
    public Photo[] photos { get; set; }
    public object customPoint { get; set; }
    public Additional3 additional { get; set; }
}

public class Descriptiondata4
{
    public string text { get; set; }
    public Translate3 translate { get; set; }
}

public class Translate3
{
    public string sourceLanguage { get; set; }
    public string targetLanguage { get; set; }
    public string text { get; set; }
}

public class Additional3
{
    public bool highlight { get; set; }
    public object parkingPlaceId { get; set; }
    public string physicalDemands { get; set; }
    public Pricelist1[] priceList { get; set; }
}

public class Pricelist1
{
    public string name { get; set; }
    public string price { get; set; }
    public string currency { get; set; }
}

public class Medium
{
    public int id { get; set; }
    public string description { get; set; }
    public object dateTime { get; set; }
    public string cdnPath { get; set; }
    public string photoPath { get; set; }
    public string thumbnailPhotoPath { get; set; }
    public int countOfLikes { get; set; }
    public bool iLikedThis { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class Photo
{
    public int id { get; set; }
    public string description { get; set; }
    public object dateTime { get; set; }
    public string cdnPath { get; set; }
    public string photoPath { get; set; }
    public string thumbnailPhotoPath { get; set; }
    public int countOfLikes { get; set; }
    public bool iLikedThis { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class Medium1
{
    public int id { get; set; }
    public string description { get; set; }
    public object dateTime { get; set; }
    public string cdnPath { get; set; }
    public string photoPath { get; set; }
    public string thumbnailPhotoPath { get; set; }
    public int countOfLikes { get; set; }
    public bool iLikedThis { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class Photo1
{
    public int id { get; set; }
    public string description { get; set; }
    public object dateTime { get; set; }
    public string cdnPath { get; set; }
    public string photoPath { get; set; }
    public string thumbnailPhotoPath { get; set; }
    public int countOfLikes { get; set; }
    public bool iLikedThis { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class Direction
{
    public string polyline { get; set; }
    public string transportType { get; set; }
    public int distance { get; set; }
    public int time { get; set; }
    public string description { get; set; }
    public Descriptiondata5 descriptionData { get; set; }
    public Additional4 additional { get; set; }
}

public class Descriptiondata5
{
    public string text { get; set; }
    public Translate4 translate { get; set; }
}

public class Translate4
{
    public string sourceLanguage { get; set; }
    public string targetLanguage { get; set; }
    public string text { get; set; }
}

public class Additional4
{
    public string physicalDemands { get; set; }
    public Pricelist2[] priceList { get; set; }
}

public class Pricelist2
{
    public string name { get; set; }
    public string price { get; set; }
    public string currency { get; set; }
}

public class Stopplace
{
    public string placeId { get; set; }
    public object introMediaId { get; set; }
}

public class Place
{
    public string placeId { get; set; }
    public Point point { get; set; }
    public string name { get; set; }
    public string localityName { get; set; }
    public string country { get; set; }
}

public class Point
{
    public float lat { get; set; }
    public float lng { get; set; }
}
