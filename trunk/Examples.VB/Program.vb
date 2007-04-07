Option Strict On

Imports NBehave.Framework.Story


    Public Class Program

        Public Shared Sub Main()
            Dim con As New ConsoleRunner(GetType(Program).Assembly)
            con.Run()

            Console.WriteLine()
            Console.ReadLine()
        End Sub

    End Class
