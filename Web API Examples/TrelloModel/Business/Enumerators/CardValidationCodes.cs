namespace TrelloModel.Business.Enumerators
{
    public enum CardValidationCodes
    {
        CardNameIsNull,
        CardNameIsEmpty,
        CardNameBiggerThanMaxValue,
        CardNameSpecialChars,
        CardDiscriptionIsEmpty,
        CardDiscpriptionIsNull,
        CardDiscriptionBiggerThanMaxValue,
        CardDiscriptionSpecialChars,
        CardIndexNegative,
        CardCreationDateSuperiorToDueDate
    }
}