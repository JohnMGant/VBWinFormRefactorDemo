Namespace Repositories

    Public Interface IEntityRepository(Of TEntity, TKey, TSearchCriteria)
        Function GetById(key As TKey) As TEntity
        Function Search(criteria As IEnumerable(Of TSearchCriteria)) As IEnumerable(Of TEntity)
        Function Create(item As TEntity) As TKey
        Sub Replace(key As TKey, item As TEntity)
        Sub Delete(key As TKey)
    End Interface

End Namespace