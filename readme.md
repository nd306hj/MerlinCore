# Merlin2d reference

## 1. Initial setup

Knižnica je dostupná ako NuGet package na <http://hades.cit.fei.tuke.sk:8080/v3/index.json>

prvé spustenie:

```csharp
//namespace Merlin2d.Game
GameContainer container = new GameContainer("window name", windowWidth, windowHeight);
container.SetMap("path to the .tmx file");
container.Run();
```

## 2. Basics

Pre jednoduchosť je knižnica naprogramovaná tak, aby sa za sekundu vykonal `Update()` 60x. Ak potrebujete niekde merať čas, môžete sa spoľahnúť na tento údaj, nepotrebujete používať časovače.

Inicializačná logika (__automaticky__ vykonáva engine po zavolaní `Run()`):

- načítaj mapu
  - ručne pridané veci do sveta sú zmazané, vypíše sa do konzoly upozornenie
  - ak je nastavená `IFactory`, použi ju na vytvorenie načítaných actorov z mapy
- vykonaj definované inicializačné akcie (pridané cez `AddInitAction()`)
  - (tu si môžete pridať do sveta actorov na testovanie, pri finálnej odovzdávke ale musia byť pridaní cez načítanie mapy, tu môžete robiť len špecifické nastavenia, napr. kamera, inventár...)
- nastav spôsob sledovania kamery
- spusti Update / render slučku

Nezabudnite použiť `IWorld.SetFactory`, `IWorld.SetMap`, `IWorld.ShowInventory`

## 3. InitAction

Kód, ktorý potrebujete vykonať pred samotným spustením hry, si viete pripraviť vo forme anonymných funkcií (napr. lambda expression) a pomocou `IWorld.AddInitAction(System.Action<IWorld> action)` ho uložiť. Kód je automaticky vykonaný na začiatku `Container.Run()`.

Kamera:

```csharp
//step 1:
container.SetCameraFollowStyle(CameraFollowStyle._Something_); //use code completion to see available options

//step 2:
IWorld.CenterOn(IActor); //needs to be done in an init action as actors are not available before (can be used when the game is already running to switch between multiple actors) 

```

## 4. End game condition

`IWorld.SetEndCondition(Func<IWorld, MapStatus> condition)` umožňuje zvoliť vlastnú podmienku pre skončenie hry:

- parameter je funkcia s `IWorld` vstupným parametrom a vracia `MapStatus`:
  - Ak sa hra ešte nemá skončiť, vráťte `MapStatus.Unfinished`
  - Ak sa má hra skončiť, vráťte `MapStatus.Finished` (výhra) alebo `MapStatus.Failed` (prehra)

Správu, ktorá sa zobrazí pri skončení hry viete nastaviť pomocou:

`GameContainer.SetEndGameMessage(IMessage, MapStatus)` (o správach sa dočítate nižšie)

## 5. Messages

Správu viete zobraziť na obrazovke pomocou `IWorld.ShowMessage(IMessage message)`.

Trieda `Message` (namespace `Merlin2d.Game`) už implementuje `IMessage`, takže si nemusíte implementovať vlastnú (ak potrebujete niečo navyše, čo neponúka `Merlin2d.Game.Message`, tak si ju implementujte podľa potreby)

```csharp
public Message(string text, int x, int y, int fontSize, Color color, MessageDuration messageDuration)
```

- x, y - súradnice na obrazovke
- fontSize - hmm... čo to asi bude?
- Color - farba textu, trieda v sebe definuje viacero farieb ako static properties (code completion is your friend here) alebo môžete použiť vlastné RGB / RGBA
- MessageDuration - enum, dĺžka trvania správy - `Short, Long, Indefinite`
  - ak použijete `Indefinite`, správa sa sama neodstráni zo sveta (užitočné napr. na zobrazenie života postavičiek); musíte ju odstrániť ručne cez `IWorld.RemoveMessage(IMessage)`
  - ak chcete správu aktualizovať, nemusíte vytvárať novú inštanciu, stačí zavolať `IMessage.SetText(string new Text)`

Správy môžete uchytiť nad `IActor` pomocou `IMessage.SetAnchorPoint(IActor actor)`; v tomto prípade budú súradnice počítané od polohy actora a správa sa bude hýbať spolu s ním.

## 6. Multiple levels

Engine podporuje pridanie viacerých levelov, v takomto prípade bude postup nasledovný:

- nepoužijete `GameContainer.SetMap(path)` ale `GameContainer.AddWorld("path to .tmx")`
- `GameContainer.GetWorld()` bude vyhadzovať chybu, musíte špecifikovať index - `GameContainer.GetWorld(0)`
  - svety sú zoradené v poradí, v akom ste ich pridali
- úspešné dokončenie mapy (`EndCondition` vráti `MapStatus.Finished`) automaticky prepne mapu na ďalšiu v poradí
- keďže sa podmienky medzi mapami môžu líšiť, každú treba inicializovať osobitne
