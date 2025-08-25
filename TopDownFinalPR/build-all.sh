#!/bin/bash

echo "Building TopDownFinalPR for all platforms..."

# Clean previous builds
echo "Cleaning previous builds..."
dotnet clean
rm -rf bin/Release

# Build for Windows (x64)
echo "Building for Windows x64..."
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true

# Build for macOS (x64)
echo "Building for macOS x64..."
dotnet publish -c Release -r osx-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true

# Build for Linux (x64)
echo "Building for Linux x64..."
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true

# Create macOS app bundle
echo "Creating macOS app bundle..."
mkdir -p TopDownFinalPR.app/Contents/MacOS
cp bin/Release/net8.0/osx-x64/publish/TopDownFinalPR TopDownFinalPR.app/Contents/MacOS/
cp -r bin/Release/net8.0/osx-x64/publish/Content TopDownFinalPR.app/Contents/

# Create Info.plist
cat > TopDownFinalPR.app/Contents/Info.plist << 'EOF'
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
	<key>CFBundleExecutable</key>
	<string>TopDownFinalPR</string>
	<key>CFBundleIdentifier</key>
	<string>com.topdownfinalpr.game</string>
	<key>CFBundleName</key>
	<string>TopDownFinalPR</string>
	<key>CFBundlePackageType</key>
	<string>APPL</string>
	<key>CFBundleShortVersionString</key>
	<string>1.0</string>
	<key>CFBundleVersion</key>
	<string>1.0</string>
	<key>LSMinimumSystemVersion</key>
	<string>10.15</string>
	<key>NSHighResolutionCapable</key>
	<true/>
</dict>
</plist>
EOF

# Create DMG if create-dmg is available
if command -v create-dmg &> /dev/null; then
    echo "Creating DMG file..."
    create-dmg --volname "TopDownFinalPR" --window-pos 200 120 --window-size 800 400 --icon-size 100 --icon "TopDownFinalPR.app" 200 190 --hide-extension "TopDownFinalPR.app" --app-drop-link 600 185 "TopDownFinalPR.dmg" "TopDownFinalPR.app/"
else
    echo "create-dmg not found. Skipping DMG creation."
fi

# Create distribution folders
echo "Creating distribution folders..."
mkdir -p dist/windows
mkdir -p dist/macos
mkdir -p dist/linux

# Copy Windows files
cp -r bin/Release/net8.0/win-x64/publish/* dist/windows/

# Copy macOS files
cp -r TopDownFinalPR.app dist/macos/
if [ -f "TopDownFinalPR.dmg" ]; then
    cp TopDownFinalPR.dmg dist/macos/
fi

# Copy Linux files
cp -r bin/Release/net8.0/linux-x64/publish/* dist/linux/

echo "Build complete!"
echo "Distribution files are in the 'dist' folder:"
echo "  - Windows: dist/windows/"
echo "  - macOS: dist/macos/"
echo "  - Linux: dist/linux/"
echo ""
echo "Files created:"
echo "  - Windows: TopDownFinalPR.exe (in dist/windows/)"
echo "  - macOS: TopDownFinalPR.app and TopDownFinalPR.dmg (in dist/macos/)"
echo "  - Linux: TopDownFinalPR executable (in dist/linux/)"
