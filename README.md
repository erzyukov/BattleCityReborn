# BattleCityReborn
Ремейк игры BattleCity для тестового задания. Первый уровень.

## Билд проекта
Версия Unity - 2020.3.14f1

В настройках билда должны быть указыны сцены в следующем порядке:
1. MenuScene
2. GameScene


## Настройки
В папке /Assets/Settings/ лежат настройки игры. К каждой настройке есть описание в инспекторе (Tooltip).

* **GameSettings** - основные настройки игры
* **PlayerOne** - Настройки анимации первого игрока
* **PlayerTwo** - Настройки анимации второго игрока (на данный момент второй игрок визуально не отличается от первого)
* **Enemy1** - Настройки первого врага
* **Enemy2** - Настройки второго врага
* **PlayerOneInputSettings** - Настройки клавиш управления персонажем первого игрока
* **PlayerTwoInputSettings** - Настройки клавиш управления персонажем второго игрока
* **PowerUpDestroyer** - Настройки усиления уничтожения противников на карте
* **PowerUpExtraLife** - Настройки усиления дополнительной жизни
* **PowerUpImmortal** - Настройки усиления бессмертия
* **PowerUpRepair** - Настройки усиления починки бункера
* **PowerUpStun** - Настройки усиления остановки всех противников
* **PowerUpUpgrade** - Настройки усиления поднятия уровня персонажа


## Скрипты
Скрипты лежат папке /Assets/Scripts/. И разбиты по папкам в соответствии с тем к какой сущности они относятся.

* **Bullet** - Пуля
* **Core** - Базовые компоненты
* **Enemy** - Враги
* **Explode** - Взрыв
* **Game** - Состояние игры
* **HQ** - Бункер
* **Interfaces** - Интерфейсы
* **Map** - Уровень
* **Menu** - Главное меню
* **Player** - Игрок
* **PowerUp** - Усиления
* **Settings** - Настройки игры
* **Spawners** - Спавнеры
* **UI** - Пользовательский интерфейс
* **Utils** - Вспомогательные классы


## Уровень
В качестве уровня используется TileMap.

Тайлы и палитра лежать в папке /Assets/Tiles/.


## Настройки управления
### Главное меню
* **W/S | UP/Down** - Выбор количиства игроков
* **Enter** - Начать игру

### Первый игрок
* **WASD** - Передвижение
* **Spase** - Стрельба

### Первый игрок
* **Arrows** - Передвижение
* **Enter** - Стрельба

### Игровая сцена
* **Esc** - Пауза

### Экран подсчета очков
* **R** - Вернуться в главное меню
