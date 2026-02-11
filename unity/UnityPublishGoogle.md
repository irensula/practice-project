# Google Play Delivery Requirements (Unity) 

## 1. Unity Environment Setup 

Required Unity Modules 

In Unity Hub: 

    Installs → Add Modules 

    Enable: 

    Android Build Support 

    Android SDK & NDK Tools 

    OpenJDK 

 

## 2. Build Configuration (Unity) 

Switch Platform 

File → Build Settings 
 

    Select Android 

    Click Switch Platform 

    Add all required scenes to Scenes in Build 

 

## 3. Android App Bundle (AAB) 

Google Play requires all new applications to be published as an Android App Bundle (.aab) instead of APK. 

Configuration Steps 

    Open File → Build Profiles 

    Select Android 

    Open Player Settings 

    In Publishing Settings, enable Split Application Binary 

    In Build Profiles → Platform Settings, enable Build App Bundle (Google Play) 

Notes 

    Build App Bundle (Google Play) is visible only when Export Project is disabled 

    Development Build must be disabled, otherwise upload may fail 

When building, Unity generates an .aab file. 

 

## 4. Application Size 

    The base module inside the AAB must be ≤ 200 MB 

    If asset packs are used, they must comply with Google Play size limits 

 

## 5. Symbols File Size 

Google Play limits the size of symbols packages. 

Unity shows a warning if symbols exceed the limit defined in: 

Player Settings → Android → Other Settings → Configuration → Symbols size threshold 
 

Symbols can be uploaded separately as a .zip file. 

 

## 6. Texture Compression Targeting 

Texture compression targeting allows Google Play to deliver optimized textures per device and reduce the base module size. 

Key Points 

    Automatically enables split application binary 

    Creates an install-time asset pack (UnityTextureCompressionsAssetPack) 

    Reduces base module size (important due to 200 MB limit) 

How to Enable 

    Enable Android App Bundles 

    Open Build Profiles 

    Go to Player Settings → Other Settings → Rendering 

    Add required Texture Compression Formats (first one is default) 

    In Build Profiles → Asset Import Overrides, set Texture Compression to anything except Force Uncompressed 

Notes 

    When enabled, Unity ignores the Android Texture Compression build setting 

    Individual textures can override compression format if needed 

 

## 7. 64-bit Architecture 

Google Play requires 64-bit support. 

Steps 

    Open Build Profiles → Android 

    Go to Player Settings → Other Settings → Configuration 

    Set Scripting Backend to IL2CPP 

    Enable ARM64 

 

## 8. Target API Level 

Applications must target the minimum API level required by Google Play. 

Steps 

    Open Build Profiles → Android 

    Go to Player Settings → Identification 

    Set Target API Level to the required (or higher) level 

 

## 9. App Identification 

Identification 

    Company Name and Product Name set 

    Package Name: 

com.companyname.gamename 
 

(Cannot be changed after first publication) 

    Version Code: Integer value, incremented on every release 

    Version Name: Human-readable version (e.g. 1.0.0, 1.0.1) 

 

## 10. App Signature & Security 

Keystore 

    Create a new Keystore: 

Player Settings → Publishing Settings → Keystore Manager 
 

    Keystore is securely backed up 

    Alias created with valid password 

Google Play App Signing 

    Must be enabled in Google Play Console 

 

## 11. Report Application Dependencies 

Google Play checks Package Manager and Asset Store dependencies for known issues. 

Steps 

    Open Build Profiles → Android 

    Go to Player Settings → Publishing Settings 

    Enable Report Dependencies in App Bundle 

 

## 12. Build the App Bundle 

    Enable Build App Bundle (Google Play) 

    Enable Create symbols.zip 

    Set symbols to Public 

    Click Build 

    Select target location 

Output Files 

    .aab file 

    symbols.zip file 

 

Google Play Console Setup 

## 13. Create App 

Go to: 

https://play.google.com/console 
 

Steps: 

    Create app 

    App name 

    Default language 

    App or game 

    Free or paid 

    Accept declarations 

 

## 14. App Setup (Dashboard) 

Complete all required sections: 

    Privacy Policy (URL required) 

    App access (test login if required) 

    Ads declaration 

    Content rating 

    Target audience 

    News apps (if applicable) 

    Data Safety 

    Government app (usually No) 

    App category and contact details 

    Main store listing: 

    Name 

    Short & full description 

    Icon 

    Feature graphic 

    Screenshots 

 

## 15. Internal Testing 

Create Internal Test 

    Go to Internal Testing 

    Create testers list 

    Add Google email addresses 

Upload Build 

    Releases → Create release 

    Upload .aab 

    Upload symbols.zip (native symbols) 

    Add release notes 

    Review release 

Send the generated link to testers to install and test the app. 

 

## 16. Production Release 

Steps 

    Go to Production 

    Select countries / regions 

    Create new release 

    Add from library (select uploaded app bundle) 

    Review release 

    Start rollout to production 

Google review may take several days. 

 

## 17. Managed Publishing (Optional) 

To control when the app goes live: 

Publishing overview → Turn on managed publishing 
 

 

## 18. Publishing an Update 

Steps 

    Open Player Settings 

    Update: 

    Version Name (e.g. 1.1.2) 

    Bundle Version Code (increment by 1) 

    Enter Keystore and key passwords 

    Build a new .aab 

    Upload via Create new release 

[Unity Instuctions](https://docs.unity3d.com/6000.3/Documentation/Manual/android-distribution-google-play.html)

[Instructions Unity + Google](https://www.youtube.com/watch?v=UXl_C3ZnRLc)