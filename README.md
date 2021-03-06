## Guessr
![Version Badge](https://img.shields.io/static/v1?label=Version&message=%20Alpha&color=blue&style=flat-square) ![Target Framework](https://img.shields.io/badge/dynamic/xml?color=%23512bd4&label=target&query=%2F%2FTargetFramework%5B1%5D&url=https%3A%2F%2Fraw.githubusercontent.com%2FStanlsSlav%2FGuessr%2Fmaster%2FOpenTriviaAPICaller.csproj&logo=.net&style=flat-square) ![Issues Count](https://img.shields.io/github/issues/StanlsSlav/Guessr?style=flat-square)

### What is Guessr?
Guessr is an **console** application which calls the [OpenTrivia's API](https://opentdb.com/) to retrieve... well... the trivia questions

### HowTo
Aside from the simplistic UI, while in game the answer can either be explicitly inputted (write the whole answer) or its corresponding number.Anything else will be treated as an incorrect answer and will fail the current question.
All input will need to be registered (write and press {Enter}) unless an error from the API was caught, which detects the keyboard events.

### Commands
Guessr holds some extra commands which work only during the gameplay:
| Command | Action |
| :---: | :---: |
| **b** or **back** | Returns the user to the main menu |
| **q** or **quit** | Exits the application |

### Todo
- *DRY* the code in file "ParseToken.cs"
- Refactor the way it's handling the token (works, but hardly)

***P.S.*** Don't request token after token after token after token because at its current state it can be broken fairly easily
- While in the menus, succesivly  
- Playing without an token will result in an thrown exception
