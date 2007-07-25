Option Strict On
Option Explicit On


Imports NBehave.Framework
Imports NBehave.Framework.World
'Imports Example.ATM.Scenarios
Imports Examples.VB.ATM.Domain
Imports NBehave.Framework.Story




Public Class UserWithdrawsCash
    Inherits Story(Of IAccount)


    'Moving money between accounts
    Public Overrides Sub Story()
        AsA("Bank card holder").IWant("to transfer money from my savings account").SoThat("I can get cash easily from an ATM")
    End Sub


    Public Overrides Sub Scenarios()

       
    End Sub


End Class