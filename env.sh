# 'adb' must be in the PATH
export ANDROID_SDK=$HOME/Android/Sdk
export PATH=$ANDROID_SDK/platform-tools/:$PATH

# JDK 11 (not anything higher or lower) must be in the path
export JDK11=/home/pleonex/Programs/jdk-11.0.2
export PATH=$JDK11/bin:$PATH

# .NET 7.0.20X must be installed (Fedora provides 10X only)
export DOTNET_ROOT=/home/pleonex/Programs/dotnet-sdk-7.0.202-linux-x64
export PATH=$DOTNET_ROOT:$DOTNET_ROOT/tools:$PATH

# Install workload
# dotnet workload install maui-android
#
# Build project
# dotnet build /p:AndroidSdkDirectory=$ANDROID_SDK
#
# Run project in first device available in 'adb devices' (emulator or phone)
# dotnet build -t:Run -f net7.0-android /p:AndroidSdkDirectory=$ANDROID_SDK
#
# Watch with hot-reload
# dotnet watch build -t:Run -f net7.0-android /p:AndroidSdkDirectory=$ANDROID_SDK
