Imports VBWinFormRefactorDemo.Models

Namespace Repositories
    Public Interface IWidgetRepository
        Inherits IEntityRepository(Of Widget, Integer, WidgetCriteria)

    End Interface

End Namespace