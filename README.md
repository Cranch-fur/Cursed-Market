<p align="center"><img src=".github/readme/banne.svg" alt="Cursed Market" width="100%"></p>

A multifunctional program providing free, temporary access to all in-game privileges: iconic characters, their customization elements, and gameplay modifiers (items, addons, and perks). During gameplay interactions, internet requests are intercepted before reaching the game servers, reducing the risk of penalties for "rule-breaking".

Download the [latest release](https://github.com/Cranch-fur/Cursed-Market/releases/latest), unpack it anywhere and press "Start" before launching the game.

<p align="center"><img src=".github/readme/feature.svg" alt="Features" width="100%"></p>

## Extra Logic
Every `*.csx` file placed into the "Extra Logic" folder next to "Cursed Market.exe" is a module (sub-folders are reserved for shared files included with `#load`). Several modules can hook the same event, they run in alphabetical order of file names (prefix names with numbers to control the order).

Extra Logic is off by default: hold Shift while pressing "Start" to open the Extra Logic window. It lists all modules with checkboxes; selecting a module shows its summary: author's description plus behaviour detected by an automatic code scan (potentially dangerous modules are marked with `[!]`). Nothing is ticked by default. Your ticks are remembered; new or changed files (including files they `#load`) appear unticked and marked "(new)" / "(changed)" until you review them. Full documentation: "Extra Logic Guide.txt" in the "Extra Logic" folder.

Describe your module with a header at the top of the file:
```csharp
// @name        Response Logger
// @author      Cranch
// @version     1.0
// @description Writes URLs of game responses to Extra Logic\Logs.
// @description Doesn't change any traffic.

OnBeforeResponse(session =>
{
    if (IsGameSession(session))
        Log(session.fullUrl);
});
```

Available to modules: `OnBeforeRequest`, `OnBeforeResponse`, `OnAfterSessionComplete`, `OnStop` (handlers are protected with try/catch and removed when the proxy stops), `IsGameSession`, `Log`, `Notify`, `ShowMessage`, `Storage` / `SaveStorage()` / `UpdateStorage(...)` (persistent JSON per module; use `UpdateStorage` when changing it inside proxy handlers, they run in parallel), `UserId`, `MatchId`, `IsInQueue`, `IsInMatch`, `PlayerRole`, `GamePlatform`. Old-style modules ("BeforeRequest.csx" etc.) that subscribe to `FiddlerApplication` directly keep working.

## Simple Timer
Can't always measure the required time interval correctly, missing by just a few seconds? To enable this feature, launch the program using "Cursed Market - Timer Feature.bat" or run "Cursed Market.exe" with the "-timerToggleFeature" launch parameter.

During the match, press the MMB (Middle Mouse Button) to start a 60-second countdown timer. You don't have to wait until it finishes; simply press the MMB again.

## Adaptive Crosshair
When option is active, the crosshair will be displayed as long as the right mouse button (RMB) is held down, and it will hide when the button is released. To enable this feature, launch the program using "Cursed Market - Crosshair Feature.bat" or run "Cursed Market.exe" with the "-crosshairToggleFeature" launch parameter.

## Troubleshooting
No internet after closing the program: press "Disable Proxy" in settings.

Nothing changes in game: turn off VPN, Fiddler and similar tools, and make sure "Use setup script" is disabled in Windows proxy settings.
