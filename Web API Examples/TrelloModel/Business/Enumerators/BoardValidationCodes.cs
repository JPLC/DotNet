namespace TrelloModel.Business.Enumerators
{
    public enum BoardValidationCodes
    {
        BoardNameSpecialChars,
        BoardNameBiggerThanMaxValue,
        BoardNameIsNull,
        BoardNameIsEmpty,
        BoardNameAlreadyExists,
        BoardDiscriptionSpecialChars,
        BoardDiscriptionBiggerThanMaxValue,
        BoardDiscpriptionIsNull,
        BoardDiscriptionIsEmpty
    }
}