# Cryptess

Test assignment for DCT company. This is a WPF program that displays various information related to cryptocurrencies.

## Description

This app is implemented using MVVM pattern with help of [MvvmCross](https://www.mvvmcross.com) framework. Therefore, it consists of the "Core" .NET Standard 2.0 library, in which all ViewModels and other platform-independent code is stored, and WPF .NET 6.0 project.

## Roadmap

- [ ] Create boilerplate code with fundamental architecture
- [ ] Create a ViewModel for main page
- [ ] Create service that will pull test data from local JSON file instead of API call
- [ ] Create a View for main page
- [ ] Create ViewModel and View for details page and navigation to and from it
- [ ] Implement searching for currency by name or code
- [ ] Replace dummy data service with real one
- [ ] Make visual improvements to the UI
- Implement additional features:
    - [ ]	Displaying quote charts for currencies 
    - [ ]	Page in which you can convert one currency to another
    - [ ]	Light / dark theme support
    - [ ]	Support for multiple localizations
