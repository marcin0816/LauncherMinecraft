# Minecraft Non-Premium Launcher v1.1 Beta

## O programie
Minecraft Non-Premium Launcher to lekki i funkcjonalny launcher Minecraft, który umożliwia grę w trybie offline. Launcher automatycznie pobiera wszystkie potrzebne pliki gry, zarządza bibliotekami i zasobami, oraz pozwala na łatwe uruchamianie różnych wersji Minecraft.

## Zmiany w wersji 1.1 Beta

### Naprawione błędy
- **Zapamiętywanie ustawień:** Naprawiono problem z niezapisywaniem i niewczytywaniem konfiguracji przy ponownym uruchomieniu
- **Ścieżka Java:** Poprawiono błędy związane z nieprawidłowym zapisywaniem i wczytywaniem ścieżki Java
- **Przydział pamięci:** Naprawiono problem z niezapamiętywaniem ustawień przydziału pamięci RAM
- **Nazwa użytkownika:** Rozwiązano problem z niezapamiętywaniem nazwy użytkownika
- **NullReferenceException:** Dodano zabezpieczenia przed wyjątkami typu null podczas zapisywania konfiguracji

### Nowe funkcje
- **System konfiguracji:** Pełne wsparcie zapisywania i wczytywania ustawień do pliku JSON
- **Obsługa wielu języków:** Dodano pełne wsparcie dla języka polskiego i angielskiego (wczytywane automatycznie)
- **System logowania:** Dodano rozbudowany system logowania działań programu do plików dziennika
- **Automatyczne wykrywanie Java:** Ulepszony system automatycznego wykrywania i zapisywania ścieżki Java
- **Zapamiętywanie wersji:** Launcher zapamiętuje ostatnio wybraną wersję Minecraft

### Usprawnienia
- **Lepsze zarządzanie błędami:** Rozbudowana obsługa wyjątków i zabezpieczenia przed awariami
- **Optymalizacja inicjalizacji:** Przebudowany proces uruchamiania dla większej niezawodności
- **Komunikaty diagnostyczne:** Dodano szczegółowe komunikaty diagnostyczne ułatwiające rozwiązywanie problemów
- **Wsparcie różnych wersji Java:** Lepsze wykrywanie i obsługa różnych wersji Java instalowanych w systemie

## Instalacja i uruchomienie
1. Pobierz najnowszą wersję launchera
2. Rozpakuj pliki do wybranego katalogu
3. Uruchom plik MCLauncher.exe
4. Wprowadź swoją nazwę użytkownika
5. Wybierz wersję Minecraft, którą chcesz pobrać/uruchomić
6. Kliknij "Pobierz wersję" lub "Uruchom grę"

## Wymagania systemowe
- Windows 7/8/10/11
- .NET Framework 4.7.2 lub nowszy
- Minimum 2GB RAM (zalecane 8GB)
- Java 8 lub nowsza (preferowane Java 17+)
- Karta graficzna z obsługą OpenGL 2.0+

## Znane problemy
- **Błąd "Invalid session id"** - normalny komunikat dla trybów offline, można bezpiecznie zignorować
- **Ostrzeżenia shaderów** - niektóre karty graficzne mogą pokazywać ostrzeżenia o nieobsługiwanych samplerach, nie wpływa to na rozgrywkę
- **Java 17 lub nowsza** - wymagana dla najnowszych wersji Minecraft (1.18+)

## Plany rozwoju
- Wsparcie dla modów i paczek modów
- Zaawansowane zarządzanie profilami gry
- Automatyczne aktualizacje launchera
- Wsparcie dla dodatkowych języków
- Integracja z popularnymi platformami modów
- Integracja z wbudowanym antycheatem dla serwerów z licencją laucnhera

## Licencja
Ten projekt jest oprogramowaniem open source dostępnym na licencji MIT.

