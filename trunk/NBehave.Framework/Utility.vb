Option Strict On

Public Class Utility

    Public Shared Function CamelCaseToNormalSentence(ByVal text As String) As String
        Dim newText As New Text.StringBuilder(text.Substring(0, 1).ToUpper)
        Dim workString As New Text.StringBuilder(text)
        Const upperCase As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ≈ƒ÷» ¿¬‘“"
        Dim i As Int32 = 1
        Do While i < text.Length
            If upperCase.IndexOf(workString.Chars(i)) >= 0 Then
                newText.Append(" "c)
                newText.Append(workString.Chars(i).ToString.ToLower)
            Else
                newText.Append(workString.Chars(i))
            End If
            i += 1
        Loop
        newText.Append(".")

        Return newText.ToString

    End Function

End Class
