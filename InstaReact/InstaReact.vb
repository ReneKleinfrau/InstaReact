Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome

Module InstaReact

    '' You have to edit this part
    Public UserNameInsta As String = ""
    Public PasswordInsta As String = ""
    Private ProfileToUse As String = "Bot" 'A Chromeprofile will get created and used to keep your cookies- Empty or "UseBlankProfile = False" to don't use a profile
    Private DoLike As Boolean = True 'If the bot should like posts
    Private DoFollow As Boolean = True 'If the bot should follow accounts
    Private DoComment As Boolean = True 'If the bot should write comments
    Private UseBlankProfile As Boolean = False 'If you don't want to stay logged in
    Private RunWithoutChrome As Boolean = False 'Headlessmode
    Public ChanceToLike As Integer = 80 'In % (0-100)
    Public ChanceToFollow As Integer = 60 'In % (0-100)
    Public ChanceToComment As Integer = 60 'In % (0-100)
    Private HashTags() As String = {"Dogs", "Cats", "Animals"}
    Private Comments() As String = {"Love this!", "So cute!", "This is not a bot!"}
    '' Edit finished

    ''' Don't touch this part if you don't know what you do
    Public Login As New System.Timers.Timer
    Public Scraper As New System.Timers.Timer
    Public NavigateToHash As New System.Timers.Timer
    Public CookieKiller As New System.Timers.Timer
    Public NavigateToFirst As New System.Timers.Timer
    Private BotStuff As New System.Timers.Timer
    Public CommentPoster As New System.Timers.Timer
    Public lbFoundPosts As New ListBox
    Public ChanceForLike As New List(Of String)
    Public ChanceForFollow As New List(Of String)
    Private rdm As New Random
    Public LikesDone As Integer = 0
    Public FollowsDone As Integer = 0
    Public CommentsDone As Integer = 0
    Public TimeOutTry As Integer = 0
    Private HashTagToUse As String = "" 'Dont touch - Use The Comments() Array
    Public Bot As IWebDriver
    Private AllOptions As New ChromeOptions
    Public Time As String = " " & DateTime.Now.ToString("dd/MM/yyyy HH:mm") & ": "



    Private Sub LoginTick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)

        If (UseBlankProfile = True Or ProfileToUse = "") Then 'Falls Leeres Profil oder keins gewählt
            DetectAndRemoveCookiePopUp()
            Try
                LoginAccount()

                Dim Hashtag As String = HashTagToUse
                Hashtag = Hashtag.Replace("#", "")
                Bot.Navigate().GoToUrl("https://www.instagram.com/explore/tags/" & Hashtag & "/")
                Login.Enabled = False
            Catch
            End Try

        Else 'Sonst falls Neues Profil, und Login ist noch vorhanden
            DetectAndRemoveCookiePopUp()
            Try
                LoginAccount()
                NavigateToHash.Enabled = True
                Login.Enabled = False
            Catch
            End Try

        End If

        Try
            If CheckIfLoginIsVisible() = False Then
                Console.WriteLine(Time & "Already logged in")
                NavigateToHash.Enabled = True
                Login.Enabled = False
            End If
        Catch
            NavigateToHash.Enabled = True
            Login.Enabled = False
        End Try
    End Sub

    Private Sub ScraperTick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Dim js As IJavaScriptExecutor = CType(Bot, IJavaScriptExecutor)
        For index As Integer = 1 To 50
            js.ExecuteScript("window.scrollTo(0, 3000)") 'Change 3000 to a higher number, if you want to scrape more posts
        Next
        Console.WriteLine(Time & "Scrolled down and now grabbing posts")
        For Each item In Bot.FindElements(By.TagName("a"))
            If (item.GetAttribute("href").Contains("/p/")) Then
                lbFoundPosts.Items.Add(item.GetAttribute("href"))
            End If
        Next

        If (lbFoundPosts.Items.Count > 0) Then
            RemoveAlreadySeenLinks()
            NavigateToFirst.Enabled = True
        End If

        Console.WriteLine(Time & "Removed already used links")
        Scraper.Enabled = False
    End Sub

    Private Sub NavigateToHashTick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        HashTagToUse = GetRandomEntry(HashTags)
        Dim Hashtag As String = HashTagToUse
        Hashtag = Hashtag.Replace("#", "")
        Bot.Navigate().GoToUrl("https://www.instagram.com/explore/tags/" & Hashtag & "/")
        Console.WriteLine(Time & "Navigated to Hashtag (" & HashTagToUse & ")")
        Scraper.Enabled = True
        NavigateToHash.Enabled = False
    End Sub

    Private Sub CookieKillerTick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        DetectAndRemoveCookiePopUp()
        DetectAndRemoveNotifyPopUp()
    End Sub


    Private Sub NavigateToFirstTick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        If (lbFoundPosts.Items.Count > 0) Then
            lbFoundPosts.SelectedIndex = 0
            Bot.Navigate().GoToUrl(lbFoundPosts.Items(lbFoundPosts.SelectedIndex).ToString)
            Console.WriteLine(Time & "Navigated to " & lbFoundPosts.Items(lbFoundPosts.SelectedIndex).ToString)

            Dim fileName As String = My.Computer.FileSystem.CurrentDirectory & "\AlreadySeen.txt"
            Dim fi As New IO.FileInfo(fileName)
            Using sw As StreamWriter = fi.AppendText()
                sw.WriteLine(lbFoundPosts.Items(lbFoundPosts.SelectedIndex).ToString)
            End Using

            lbFoundPosts.Items.Remove(lbFoundPosts.Items(lbFoundPosts.SelectedIndex))
            BotStuff.Enabled = True
            NavigateToFirst.Enabled = False
        Else
            NavigateToHash.Enabled = True
            NavigateToFirst.Enabled = False
            Console.WriteLine(Time & "Bot loop restarted")

            'Restart Bot-Loop
        End If

    End Sub

    Private Sub BotStuffTick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)


        CalculateChanceForLike()
        CalculateChanceForFollow()

        If (CheckIfFollowButtonIsVisible() = True) Then 'Wait for the followbutton
            TimeOutTry = 0
            If (DoLike = True) Then
                If (ChanceForLike(GetRandom(1, 100)) = "True") Then
                    LikePost()
                Else
                    Console.WriteLine(Time & "Skipped like (Because of chance)")
                End If
            End If

            If (DoFollow = True) Then
                If (ChanceForFollow(GetRandom(1, 100)) = "True") Then
                    FollowAccount()
                Else
                    Console.WriteLine(Time & "Skipped Follow (Because of chance)")
                End If
            End If

            If (DoComment = True) Then
                CommentPoster.Enabled = True
                BotStuff.Enabled = False
            Else
                NavigateToFirst.Enabled = True
                BotStuff.Enabled = False
            End If

        Else
            TimeOutTry = TimeOutTry + 1
        End If

        If (TimeOutTry = 5) Then 'Move on if the followbutton cannot be found within 5 Tries.
            Console.WriteLine(Time & "Could not find the follow-button (collab?). Skipped")
            NavigateToFirst.Enabled = True
            BotStuff.Enabled = False
        End If

    End Sub

    Private Sub CommentPosterTick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Dim ChanceForComment As New List(Of String)
        For ChanceForCommentTrue As Integer = 1 To ChanceToComment
            ChanceForComment.Add("True")
        Next
        For ChanceForCommentFalse As Integer = 1 To 100 - ChanceToComment
            ChanceForComment.Add("False")
        Next

        If (ChanceForComment(GetRandom(1, 100)) = "True") Then
            Dim CommentToPost As String = GetRandomEntry(Comments)
            PostComment(CommentToPost)
        Else
            Console.WriteLine(Time & "Skipped Comment (Because of chance)")
            NavigateToFirst.Enabled = True
            CommentPoster.Enabled = False
        End If
    End Sub

    Sub Main()
        Login.AutoReset = True
        Login.Interval = 5000
        AddHandler Login.Elapsed, AddressOf LoginTick

        Scraper.AutoReset = True
        Scraper.Interval = 5000
        AddHandler Scraper.Elapsed, AddressOf ScraperTick

        NavigateToHash.AutoReset = True
        NavigateToHash.Interval = 5000
        AddHandler NavigateToHash.Elapsed, AddressOf NavigateToHashTick

        CookieKiller.AutoReset = True
        CookieKiller.Interval = 2000
        AddHandler CookieKiller.Elapsed, AddressOf CookieKillerTick

        NavigateToFirst.AutoReset = True
        NavigateToFirst.Interval = 1000
        AddHandler NavigateToFirst.Elapsed, AddressOf NavigateToFirstTick

        BotStuff.AutoReset = True
        BotStuff.Interval = 5000
        BotStuff.Enabled = False
        AddHandler BotStuff.Elapsed, AddressOf BotStuffTick

        CommentPoster.AutoReset = True
        CommentPoster.Interval = 3000
        AddHandler CommentPoster.Elapsed, AddressOf CommentPosterTick


        If (UseBlankProfile = True Or ProfileToUse = "") Then
        Else
            AllOptions.AddArgument("--user-data-dir=" & My.Computer.FileSystem.CurrentDirectory & "\Profile\" & ProfileToUse)
        End If
        If (RunWithoutChrome = True) Then
            AllOptions.AddArgument("--headless")
        End If
        AllOptions.AddArguments("--window-size=1920,1080")
        AllOptions.AddArguments("--disable-gpu")
        AllOptions.AddArguments("--disable-extensions")
        AllOptions.AddArguments("--no-sandbox")
        AllOptions.AddArguments("--start-maximized")
        AllOptions.AddArguments("--disable-web-security")
        AllOptions.AddArguments("--no-sandbox")
        AllOptions.AddArguments("--disable-plugins-discovery")
        AllOptions.AddArguments("--disable-blink-features=AutomationControlled")
        AllOptions.AddArguments("--user-agent=" & RandomUserAgent())

        Dim service As ChromeDriverService = ChromeDriverService.CreateDefaultService()
        service.HideCommandPromptWindow = True

        If (RunWithoutChrome = True) Then
            Bot = New ChromeDriver(service, AllOptions)
        Else
            Bot = New ChromeDriver(AllOptions)
        End If

        Bot.Navigate().GoToUrl("https://www.instagram.com/")

        Console.Clear()
        Console.ForegroundColor = ConsoleColor.Magenta
        Console.WriteLine("")
        Console.WriteLine(" ██╗███╗   ██╗███████╗████████╗ █████╗ ██████╗ ███████╗ █████╗  ██████╗████████╗")
        Console.WriteLine(" ██║████╗  ██║██╔════╝╚══██╔══╝██╔══██╗██╔══██╗██╔════╝██╔══██╗██╔════╝╚══██╔══╝")
        Console.WriteLine(" ██║██╔██╗ ██║███████╗   ██║   ███████║██████╔╝█████╗  ███████║██║        ██║   ")
        Console.WriteLine(" ██║██║╚██╗██║╚════██║   ██║   ██╔══██║██╔══██╗██╔══╝  ██╔══██║██║        ██║   ")
        Console.WriteLine(" ██║██║ ╚████║███████║   ██║   ██║  ██║██║  ██║███████╗██║  ██║╚██████╗   ██║   ")
        Console.WriteLine(" ╚═╝╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝ ╚═════╝   ╚═╝   ")
        Console.WriteLine("")
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine(" [V0.0.2a]InstaReact has been started")
        Console.WriteLine(" --------------------------------------------------------------------")
        Console.ResetColor()
        Console.WriteLine(Time & "Bot is running...")

        Scraper.Enabled = False
        NavigateToHash.Enabled = False
        NavigateToFirst.Enabled = False
        CommentPoster.Enabled = False
        Login.Enabled = True
        CookieKiller.Enabled = True

        Console.ReadLine()
    End Sub

End Module
