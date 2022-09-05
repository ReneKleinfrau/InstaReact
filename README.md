# InstaReact
InstaReact is a Selenium-based Console-Application for Like/Follow/Comment Automation on Instagram      

![Console output](https://i.imgur.com/7WnGkcn.png "Console output")

## Installation

Once you opened the project you have to install the Selenium NuGet-Package

## Quickstart

In InstaReact.vb you can edit all settings easily

```vb.net
    Public UserNameInsta As String = ""
    Public PasswordInsta As String = ""
    Private ProfileToUse As String = "Profilename" 'If you want to keep logged in, you can use chrome profiles
    Private DoLike As Boolean = True 
    Private DoFollow As Boolean = True
    Private DoComment As Boolean = True
    Private UseBlankProfile As Boolean = False 
    Private RunWithoutChrome As Boolean = False
    Public ChanceToLike As Integer = 70 'In % (0-100)
    Public ChanceToFollow As Integer = 40 'In % (0-100)
    Public ChanceToComment As Integer = 60 'In % (0-100)
    Private HashTags() As String = {"dog", "cat", "animals"}
    Private Comments() As String = {"Very nice!", "Love this :D", "Cute!"}
```

## How to update?

In XPath.vb you can edit all XPaths to the elements. You can find them with your Browser in the developer-tools.

## Nothing happens, what can I do?

In XPath.vb you have to edit the last 3 XPaths to your language. You will see "Folgen" and "Jetzt nicht". 
This is only for the German users. You have to change these strings to your language. "Folgen" is the text of the follow-button,
"Jetzt nicht" is the button text of the Notification-PopUp.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
