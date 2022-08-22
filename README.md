# Cryptess

Test assignment for DCT company. This is a WPF program that displays various information related to cryptocurrencies.

## Description

This app is implemented using MVVM pattern with help of [MvvmCross](https://www.mvvmcross.com) framework. Therefore, it consists of the "Core" .NET Standard 2.0 library, in which all ViewModels and other platform-independent code is stored, and WPF .NET 6.0 project.

## Roadmap

- [x] Create boilerplate code with fundamental architecture
- [x] Create a ViewModel for main page
- [x] Create service that will pull test data from local JSON file instead of API call
- [x] Create a View for main page
- [x] Create ViewModel and View for details page and navigation to and from it
- [x] Implement searching for currency by name or code
- [x] Replace dummy data service with real one
- [x] Implement displaying markets on details page
- [ ] Make visual improvements to the UI
- Implement additional features:
    - [ ]   Ability to go to the currency page on the market 
    - [ ]	Displaying quote charts for currencies 
    - [ ]	Page in which you can convert one currency to another
    - [ ]	Light / dark theme support
    - [ ]	Support for multiple localizations

## Getting started

In order to run the application you need:

1. Clone this repository
2. (Optional) Open appsettings.Development.json file located at *Cryptess/Cryptess.WPF/appsettings.Development.json* with text editor and add following entries:
```json
{
  "CryptingUp": {
    "AssetsSize": 10,
    "MarketsSize": 5
}
```
Modifying "AssetsSize" you can display different numbers of assets on main page (default is 10), and "MarketsSize" corresponds to the number of markets displayed on details page (default is 5).

3. Build and Run the app in debug mode.
