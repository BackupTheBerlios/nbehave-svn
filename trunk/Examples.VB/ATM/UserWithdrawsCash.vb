Option Strict On
Option Explicit On

Imports NBehave.Framework
Imports Examples.VB.ATM.Domain


Public Class UserWithdrawsCash
    Inherits Behaviour


    Public Sub TransferToCashAccount()

        Story("Transfer to cash account"). _
            AsA("Bank card holder"). _
            IWant("to transfer money from my savings account"). _
            SoThat("I can get cash easily from an ATM")

        Scenario("Transfer money"). _
            Given("my savings account balance is", 100, AddressOf SetAccountBalance). _
            And("my cash account balance is", 200, AddressOf SetCashAccountBalance). _
            When("I transfer to cash account", 20, AddressOf TransferMoney). _
            Then("my savings account balance should be", 80, AddressOf VerifyAccountBalance). _
            And("my cash account balance should be", 220, AddressOf VerifyCashAccountBalance)


        Scenario("Transfer alot of money"). _
           Given("my savings account balance is", 1000, AddressOf SetAccountBalance). _
           And("my cash account balance is", 2000, AddressOf SetCashAccountBalance). _
           When("I transfer to cash account", 200, AddressOf TransferMoney). _
           Then("my savings account balance should be", 800, AddressOf VerifyAccountBalance). _
           And("my cash account balance should be", 2200, AddressOf VerifyCashAccountBalance)

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