Imports System.IO
Imports OpenQA.Selenium

Module Utilities


    ' Returns a Number between given parameters
    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Dim Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function

    ' Removes links out of lbFoundPosts if the link is already in AlreadySeen.txt
    Public Function RemoveAlreadySeenLinks() As Object
        For i As Integer = lbFoundPosts.Items.Count - 1 To 0 Step -1
            If (File.ReadAllText(My.Computer.FileSystem.CurrentDirectory & "\AlreadySeen.txt").Contains(lbFoundPosts.Items(i).ToString)) Then
                Console.WriteLine(Time & "Removed used link (" & lbFoundPosts.Items(i).ToString & ")")
                lbFoundPosts.Items.RemoveAt(i)
            End If
        Next
    End Function

    ' Returns a random UserAgent, should get updated more often
    Public Function RandomUserAgent() As String
        Dim rng As New System.Random()
        Dim RAND(15) As String
        RAND(0) = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36"
        RAND(1) = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.0.0 Safari/537.36"
        RAND(2) = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.5005.63 Safari/537.36"
        RAND(3) = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.134 Safari/537.36"
        RAND(4) = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36"
        RAND(5) = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36"
        RAND(6) = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.157 Safari/537.36"
        RAND(7) = "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36"
        RAND(8) = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36"
        RAND(9) = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36"
        RAND(10) = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36"
        RAND(11) = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/E7FBAF"
        RAND(12) = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Safari/537.36"
        RAND(13) = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36"
        RAND(14) = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36"
        RAND(15) = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36"
        Return RAND(rng.Next(RAND.Count())) + GetRandom(500, 2000).ToString()
    End Function

    ' Returns a random Item of a List (of Strings)
    Public Function GetRandomEntry(ByVal InputList As ICollection(Of String)) As String
        Dim rdm As New Random()
        Dim tempList As New List(Of String)(InputList)
        Return tempList(rdm.Next(0, InputList.Count))
    End Function

    ' Sets the list-content to calculate chances for Follow a user
    Public Function CalculateChanceForFollow() As Object
        ChanceForFollow.Clear()
        For ChanceForFollowTrue As Integer = 1 To ChanceToFollow
            ChanceForFollow.Add("True")
        Next
        For ChanceForFollowFalse As Integer = 1 To 100 - ChanceToFollow
            ChanceForFollow.Add("False")
        Next
    End Function

    ' Sets the list-content to calculate chances for like a post
    Public Function CalculateChanceForLike() As Object
        ChanceForLike.Clear()
        For ChanceForLikeTrue As Integer = 1 To ChanceToLike
            ChanceForLike.Add("True")
        Next
        For ChanceForLikeFalse As Integer = 1 To 100 - ChanceToLike
            ChanceForLike.Add("False")
        Next
    End Function

    ' Posts a comment
    Public Function PostComment(ByVal Comment As String) As Object
        Dim Commentbox1 As Boolean = False
        Dim CommentButton1 As Boolean = False

        ''''''''''''''CommentBox (Text area)
        Try
            Dim CommentBox As IWebElement = Bot.FindElement(By.XPath(CommentBox_1_XPath))
            CommentBox.Click()
            Commentbox1 = True
        Catch
            Try
                Dim CommentBox As IWebElement = Bot.FindElement(By.XPath(CommentBox_2_XPath))
                CommentBox.Click()

                Commentbox1 = True
            Catch ex As Exception
                Commentbox1 = False
            End Try

        End Try

        ''''''''''''''CommentBox again (text area)
        Try
            Dim CommentBoxAgain As IWebElement = Bot.FindElement(By.XPath(CommentBox_1_XPath))
            CommentBoxAgain.SendKeys(Comment)
            Commentbox1 = True
        Catch
            Try
                Dim CommentBoxAgain As IWebElement = Bot.FindElement(By.XPath(CommentBox_2_XPath))
                CommentBoxAgain.SendKeys(Comment)
                Commentbox1 = True
            Catch ex As Exception
                Commentbox1 = False
            End Try

        End Try

        System.Threading.Thread.Sleep(700)

        ''''''''''''''CommentButton (Button class)
        Try
            Dim CommentButton As IWebElement = Bot.FindElement(By.XPath(CommentButton_1_XPath))
            CommentButton.Click() 'Button Class
            CommentButton1 = True
        Catch
            Try
                Dim CommentButton As IWebElement = Bot.FindElement(By.XPath(CommentButton_2_XPath))
                CommentButton.Click() 'Button Class
                CommentButton1 = True
            Catch ex As Exception
                CommentButton1 = False
            End Try
        End Try

        ' For debugging purposes
        If (Commentbox1 = False And CommentButton1 = True) Then
            Console.WriteLine(Time & "Error with Commentbox")
        End If

        If (Commentbox1 = True And CommentButton1 = False) Then
            Console.WriteLine(Time & "Error with CommentButton")
        End If

        If (Commentbox1 = False And CommentButton1 = False) Then
            Console.WriteLine(Time & "Error with Commentbox and CommentButton")
        End If

        If (Commentbox1 = True And CommentButton1 = True) Then
            CommentsDone = CommentsDone + 1
            Console.WriteLine(Time & "Posted Comment (" & CommentsDone & ")")
            CommentPoster.Enabled = False
            NavigateToFirst.Enabled = True
        Else
            Console.WriteLine(Time & "Failed to post comment (Maybe disabled or error)")
        End If

    End Function

    ' Follows the creator of a post
    Public Function FollowAccount() As Object
        Try
            Dim FollowButton As IWebElement = Bot.FindElement(By.XPath(FollowButton_1_XPath))
            FollowButton.Click()
            FollowsDone = FollowsDone + 1
            Console.WriteLine(Time & "Followed Account (" & FollowsDone & ")")
        Catch
            Try 'Second Try
                Dim FollowButton As IWebElement = Bot.FindElement(By.XPath(FollowButton_2_XPath))
                FollowButton.Click()
                FollowsDone = FollowsDone + 1
                Console.WriteLine(Time & "Followed Account (" & FollowsDone & ")")

            Catch
                Try 'Third Try
                    Dim FollowButton As IWebElement = Bot.FindElement(By.XPath(FollowButton_3_XPath))
                    FollowButton.Click()
                    FollowsDone = FollowsDone + 1
                    Console.WriteLine(Time & "Followed Account (" & FollowsDone & ")")

                Catch ex As Exception
                    Console.WriteLine(Time & "Could not follow Account (collab-post maybe?)")
                End Try
            End Try
        End Try
    End Function

    ' Likes a post
    Public Function LikePost() As Object
        Try
            Dim LikeButton As IWebElement = Bot.FindElement(By.XPath(LikeButton_1_XPath))
            LikeButton.Click()
            LikesDone = LikesDone + 1
            Console.WriteLine(Time & "Liked post (" & LikesDone & ")")
        Catch
            Try
                Dim LikeButton As IWebElement = Bot.FindElement(By.XPath(LikeButton_2_XPath))
                LikeButton.Click()
                LikesDone = LikesDone + 1
                Console.WriteLine(Time & "Liked post (" & LikesDone & ")")
            Catch
                Try
                    Dim LikeButton As IWebElement = Bot.FindElement(By.XPath(LikeButton_3_XPath))
                    LikeButton.Click()
                    LikesDone = LikesDone + 1
                    Console.WriteLine(Time & "Liked post (" & LikesDone & ")")
                Catch ex As Exception
                    Console.WriteLine(Time & "Could not like post")
                End Try
            End Try
        End Try
    End Function

    ' Detects and removes the cookie popup
    Public Function DetectAndRemoveCookiePopUp() As Object
        Try
            If Bot.FindElement(By.XPath(CookieWindow_XPath)).Displayed = True Then
                Console.WriteLine(Time & "Cookiewindow found")
                Dim CookieButton As IWebElement = Bot.FindElement(By.XPath(CookieWindow_XPath))
                CookieButton.Click()
                Console.WriteLine(Time & "Cookiewindow destroyed")
            End If
        Catch

        End Try
    End Function

    ' Detects and removes the notify window
    Public Function DetectAndRemoveNotifyPopUp() As Object
        Try
            If Bot.FindElement(By.XPath(Notify_XPath)).Displayed = True Then 'German version
                Console.WriteLine(Time & "Notifywindow found")
                Dim CookieButton As IWebElement = Bot.FindElement(By.XPath(Notify_XPath))
                CookieButton.Click()
                Console.WriteLine(Time & "Notifywindow destroyed")
            End If
        Catch
        End Try
    End Function

    ' Enters credentials and presses the loginbutton
    Public Function LoginAccount() As Object
        Try
            Dim UserName As IWebElement = Bot.FindElement(By.XPath(LoginUser_XPath))
            UserName.Click()
            UserName.SendKeys(UserNameInsta)
            Console.WriteLine(Time & "Entered Username")

            Dim Password As IWebElement = Bot.FindElement(By.XPath(LoginPassword_XPath))
            Password.Click()
            Password.SendKeys(PasswordInsta)
            Console.WriteLine(Time & "Entered Password")

            Dim LoginButton As IWebElement = Bot.FindElement(By.XPath(LoginButton_XPath))
            LoginButton.Click()
            Console.WriteLine(Time & "Pressed Loginbutton")

            System.Threading.Thread.Sleep(3000)
        Catch
        End Try

    End Function

    ' Checks if the followbutton on a post is visible. You need to change the language if you are not German
    Public Function CheckIfFollowButtonIsVisible() As Boolean
        Try
             If Bot.FindElement(By.XPath(FollowButton_1_XPath)).Displayed = True Then
                Return (True)
            Else
                Return (False)
            End If
        Catch
            Return (False)
        End Try
    End Function

    ' Checks if the Loginbutton is visible
    Public Function CheckIfLoginIsVisible() As Boolean
        Try
            If Bot.FindElement(By.XPath(LoginUser_XPath)).Displayed = False Then
                Return (True)
            Else
                Return (False)
            End If
        Catch
            Return (False)
        End Try
    End Function

End Module
