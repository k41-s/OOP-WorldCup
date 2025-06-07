# World Cup Statistics Project

## Overview
This is a .NET-based desktop application suite for displaying statistics from the FIFA World Cup 2018 (Men) and FIFA World Cup 2019 (Women). It consists of the following components:

1. **Windows Forms Application** – Interactive UI for selecting favorite teams and players, viewing rankings, and managing preferences.
2. **WPF Application** – Responsive interface with match overviews, lineup visualization, and animated player detail windows.
3. **Data Layer (Class Library)** – Handles data access from the API or local JSON files and provides shared business logic.

---

## Features

### Windows Forms Application
- Select **preferred championship** (Men or Women) and **language** (English or Croatian)
- Choose a **favorite national team**
- Select **three favorite players** from the team’s first match
- Support for **drag-and-drop** and **context menus** to manage player selection
- Display **player details** including name, number, position, image, captain status, and favorite status
- View **ranking lists** for:
  - Top goal scorers
  - Most yellow cards
  - Matches sorted by attendance
- Export **rankings to PDF**
- Modify **application settings** via a dedicated UI
- Confirmation dialogs on exit and setting changes

### WPF Application
- Fully **responsive and animated UI**
- Support for **fullscreen and predefined resolutions**
- Display **selected national team** and an **opponent from a played match**
- View **match result** and team statistics in an animated window
- Show **starting lineups** visually on a football pitch
- Click players to open **animated player detail windows**
- All user preferences and selections are **synchronized** with the Windows Forms application

### Data Layer (Class Library)
- Retrieves data from either the **official API** or **local JSON files**
- Supports **asynchronous data loading**
- Centralized storage and parsing of:
  - Match data
  - Team statistics
  - Player events
- Loads and saves **user preferences** and **favorites** using local text files
- Graceful **error handling** for unavailable data or missing files

---

## API Endpoints

- [Men's Team Results](https://worldcup-vua.nullbit.hr/men/teams/results)
- [Women's Team Results](https://worldcup-vua.nullbit.hr/women/teams/results)
- [Men's Matches](https://worldcup-vua.nullbit.hr/men/matches)
- [Women's Matches](https://worldcup-vua.nullbit.hr/women/matches)
- Matches by country (example):
  - `https://worldcup-vua.nullbit.hr/men/matches/country?fifa_code=ENG`

> Use HTTP version if HTTPS is unavailable.

---

## Installation & Setup

1. **Clone the Repository**
   ```sh
   git clone https://github.com/your-repo-url
   cd WorldCupStatistics
   ```

2. **Open the Solution in Visual Studio**
   - Open `WorldCupStatistics.sln`
   - Restore NuGet packages if prompted

3. **Build & Run**
   - Set `WinFormsApp` or `WpfApp` as the startup project
   - Build the solution and run the application

4. **Configuration**
   - Create or modify `config.txt` to set:
     ```
     UseApi=true   # or false for offline mode
     ```

---

## Error Handling

- Displays user-friendly messages if the API or files are unavailable
- Prompts the user to reconfigure settings if required files are missing
- Uses **relative paths** to ensure compatibility across environments
- Avoids application crashes through consistent exception handling

---

## Technologies Used

- .NET 8.0
- C#
- Windows Forms
- Windows Presentation Foundation (WPF)
- Newtonsoft.Json
- System.Text.Json
- PrintDocument API
- Local file I/O
