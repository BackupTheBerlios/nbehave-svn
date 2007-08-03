Option Strict On
Option Explicit On

Imports NBehave.Framework
Imports NBehave.Framework.World
Imports Examples.VB.ATM.Domain
Imports NBehave.Framework.Story


<Behaviour()> _
Public Class UserWithdrawsCash
    Inherits Behaviour


    Public Sub TransferToCashAccount()
        Story("Transfer to cash account").AsA("Bank card holder").IWant("to transfer money from my savings account").SoThat("I can get cash easily from an ATM")

        Scenario("Transfer money"). _
        Given("my savings account balance is", 42, AddressOf SetAccountBalance). _
        [And]("my cash account balance is", 42, AddressOf SetCashAccountBalance). _
        [When]("I transfer to cash account", 42). _
        [Then]("my savings account balance should be", 0). _
        [And]("my cash account balance should be", 84)

    End Sub


    Public Sub TransferToAnotherCashAccount()
        Story.AsA("Bank card holder").IWant("to transfer money from my savings account").SoThat("I can get cash easily from an ATM")

        Scenario("Transfer money"). _
        Given("", 42). _
        [And]("", 42). _
        [When]("", 42). _
        [Then]("42", 42). _
        [And]("42", 42)
    End Sub



End Class