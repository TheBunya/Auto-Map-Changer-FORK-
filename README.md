# Auto-Map-Changer [FORK]

## RU
Auto-Map-Changer [FORK] - форк плагина (от 2024 года), который меняет на заданную вами карту (из конфига) при неактивности на сервере, но с некоторыми исправлениями. Плагин полезен тем, у кого "онлайн" на серверах только за счет одной карты.

### В этой версии было исправлено
- Смена карты повторно, даже если заданная карта в конфиге уже отображалась на сервере.
- Исправление, когда плагин независимо от конфига использовал стандартное значение 180 секунд для смены карты. Теперь любое значение, заданное пользователем, берется в расчет.

### Что есть в репозитории
- Source (исходный код).
- Уже готовый плагин в разделе Releases.
- Плагин собран на последней версии релиза CounterStrikeSharp.

### Установка
1. Установите [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp) и [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master).
2. Скачайте последний релиз: [Auto-Map-Changer [FORK] Releases](https://github.com/TheBunya/Auto-Map-Changer-FORK-/releases/latest).
3. Распакуйте архив и загрузите файлы на игровой сервер.

### Конфиг
Конфиг создается автоматически в той же папке, где находится `AutoChangeMap.dll`.

```json
{
  "Delay": 180,
  "DefaultMap": "de_dust2",
  "ChangeMap": false,
  "Debug": false
}
```

Пояснения:
- `Delay` - интервал проверки в секундах.
- `DefaultMap` - карта, на которую нужно сменить сервер.
- `ChangeMap` - если `false`, карта не меняется при наличии реальных игроков.
- `Debug` - подробные логи в консоль.

> Для Workshop-карт используйте префикс `ws:`.

### Команда
`css_acm_reload` - перезагрузка конфига `AutoChangeMap`.

## EN
Auto-Map-Changer [FORK] is a fork of the 2024 plugin that switches the server to a configured map when the server is inactive, with several fixes applied. This plugin is useful for servers where player activity mostly depends on one map.

### Fixed in this version
- Fixed repeated map changes even when the configured target map was already active on the server.
- Fixed an issue where the plugin used the default 180-second delay regardless of config. Now any user-defined value is respected.

### Repository contents
- Source code.
- Ready-to-use plugin build in Releases.
- Built with the latest CounterStrikeSharp release.

### Installation
1. Install [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp) and [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master).
2. Download the latest release: [Auto-Map-Changer [FORK] Releases](https://github.com/TheBunya/Auto-Map-Changer-FORK-/releases/latest).
3. Unpack the archive and upload files to your game server.

### Config
The config is created automatically in the same directory as `AutoChangeMap.dll`.

```json
{
  "Delay": 180,
  "DefaultMap": "de_dust2",
  "ChangeMap": false,
  "Debug": false
}
```

Options:
- `Delay` - check interval in seconds.
- `DefaultMap` - target map to switch to.
- `ChangeMap` - if `false`, the map will not be changed while real players are online.
- `Debug` - enables verbose console logging.

> For Workshop maps, use the `ws:` prefix.

### Command
`css_acm_reload` - reload `AutoChangeMap` config.
