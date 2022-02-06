# Basic ChromeDriver Detection Patch
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![dotnet6.0](https://img.shields.io/badge/.NET-6.0-blue?style=for-the-badge)
![License-MIT](https://img.shields.io/badge/License-MIT-Green?style=for-the-badge)

A tool that patches the ChromeDriver binary to avert a "basic" bot detection method used by most anti-automation services.

### Usage ([Download](https://github.com/Plot1337/BasicChromeDriverDetectionPatch/releases)):
1. Via CLI `BasicChromeDriverDetectionPatch.exe [path_to_chromedriver_exe]`
2. Or drag-and-drop `chromedriver.exe` into `BasicChromeDriverDetectionPatch.exe`

The expected output would be an executable file named `chromedriver_patched.exe`.
You can remove the original executable and renamed the modified one so Selenium can properly use it.

### How Does It Work?
It's simple; the tool replaces the "CDC" (`_cdc[...]_[...]`) IDs with alternative names.
This tool merely automates a method mentioned in this
[StackOverflow answer](https://stackoverflow.com/questions/33225947/can-a-website-detect-when-you-are-using-selenium-with-chromedriver/41220267).
Please note this tool — just like all other methods of making automation harder to detect, even ones claiming to be "undetectable" — will never actually be undetectable.

### Legal
This tool is [licensed under MIT](https://github.com/Plot1337/BasicChromeDriverDetectionPatch/blob/main/LICENSE); do whatever you want with it.

### Contact:
- Discord: `Plot#1337` (UID: `918642844179251260`)
- Telegram: `@daddyplot`
