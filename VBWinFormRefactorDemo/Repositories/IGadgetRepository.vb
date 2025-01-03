Imports VBWinFormRefactorDemo.Models

Namespace Repositories

    Public Interface IGadgetRepository
        Inherits IEntityRepository(Of Gadget, Integer, GadgetCriteria)

    End Interface

End Namespace