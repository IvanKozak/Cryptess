# Cryptess

Test assignment for DCT company. This is a WPF program that displays various information related to cryptocurrencies.

## Description

This app is implemented using MVVM pattern with help of [MvvmCross](https://www.mvvmcross.com) framework. Therefore, it consists of the "Core" .NET Standard 2.0 library, in which all ViewModels and other platform-independent code is stored, and WPF .NET 6.0 project.

## Roadmap

- [x] Create boilerplate code with fundamental architecture
- [x] Create a ViewModel for main page
- [x] Create service that will pull test data from local JSON file instead of API call
- [x] Create a View for main page
- [ ] Create ViewModel and View for details page and navigation to and from it
- [ ] Implement searching for currency by name or code
- [ ] Replace dummy data service with real one
- [ ] Make visual improvements to the UI
- Implement additional features:
    - [ ]	Displaying quote charts for currencies 
    - [ ]	Page in which you can convert one currency to another
    - [ ]	Light / dark theme support
    - [ ]	Support for multiple localizations

## Getting started

As of now this program can only work with sample data located at *Cryptess/Cryptess.Core/Repositories/AssetSamples.json*.

In order to run the application you need:

1. Clone this repository
2. Open appsettings.Development.json file located at *Cryptess/Cryptess.WPF/appsettings.Development.json* with text editor and add entry to it that shows program the path to sample file. Entry sould look something like this:
```json
{
  "DummyAssetsPath": "C:\\Users\\MyUser\\source\\repos\\MyName\\Cryptess\\Cryptess.Core\\Repositories\\AssetSamples.json"
}
```
3. Build and Run the app in debug mode.
