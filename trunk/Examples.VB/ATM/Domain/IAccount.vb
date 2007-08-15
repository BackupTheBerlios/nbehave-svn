
Public Interface IAccount

    Property Balance() As Int32
    Sub Transfer(ByVal amount As Int32, ByVal toAccount As IAccount)

End Interface
