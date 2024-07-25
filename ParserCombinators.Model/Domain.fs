namespace ParserCombinators.Model

type PieceOfNews = {
    Text : string
    Filter : string
}

type User = {
    Name : string
    IsPremium : bool
    Age : int
    Roles : string Set
}