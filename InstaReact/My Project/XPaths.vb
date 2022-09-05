Module XPaths
    'You have to update this
    Public CommentBox_1_XPath As String = "/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/section/main/div[1]/div[1]/article/div/div[2]/div/div[2]/section[3]/div/div/form/textarea"
    Public CommentBox_2_XPath As String = "/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/section/main/div[1]/div[1]/article/div/div[2]/div/div[2]/section[3]/div/form/textarea"

    Public CommentButton_1_XPath As String = "/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/section/main/div[1]/div[1]/article/div/div[2]/div/div[2]/section[3]/div/div/form/button"
    Public CommentButton_2_XPath As String = "/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/section/main/div[1]/div[1]/article/div/div[2]/div/div[2]/section[3]/div/form/button"

    Public FollowButton_1_XPath As String = "/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/section/main/div[1]/div[1]/article/div/div[2]/div/div[1]/div/header/div[2]/div[1]/div[2]/button"
    Public FollowButton_2_XPath As String = "/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/section/main/div[1]/div[1]/article/div/div[2]/div/div[1]/div/header/div[2]/div[1]/div[2]/button/div"
    Public FollowButton_3_XPath As String = "/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/section/main/div[1]/div[1]/article/div/div[2]/div/div[1]/div/header/div[2]/div[1]/div[2]/button/div/div"

    Public LikeButton_1_XPath As String = "/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/section/main/div[1]/div[1]/article/div/div[2]/div/div[2]/section[1]/span[1]/button"
    Public LikeButton_2_XPath As String = "/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/section/main/div[1]/div[1]/article/div/div[2]/div/div[2]/section[1]/span[1]"
    Public LikeButton_3_XPath As String = "/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/section/main/div[1]/div[1]/article/div/div[2]/div/div[2]/section[1]/span[1]/button/div[2]"

    Public LoginUser_XPath As String = "//*[@id=" & ControlChars.Quote & "loginForm" & ControlChars.Quote & "]/div/div[1]/div/label/input"
    Public LoginPassword_XPath As String = "//*[@id=" & ControlChars.Quote & "loginForm" & ControlChars.Quote & "]/div/div[2]/div/label/input"
    Public LoginButton_XPath As String = "//*[@id=" & ControlChars.Quote & "loginForm" & ControlChars.Quote & "]/div/div[3]/button/div"

    Public FollowButton As String = "//*[contains(text(), 'Folgen')]"
    Public CookieWindow_XPath As String = "/html/body/div[4]/div/div/button[1]"
    Public Notify_XPath As String = "//button[text()=" & ControlChars.Quote & "Jetzt nicht" & ControlChars.Quote & "]"
End Module
