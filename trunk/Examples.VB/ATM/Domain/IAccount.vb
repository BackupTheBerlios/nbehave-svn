Namespace ATM.Domain

    Public Interface IAccount

        Property Balance() As Int32
        Sub Withdraw(ByVal amount As Int32)

    End Interface

End Namespace