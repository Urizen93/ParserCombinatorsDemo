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

// We'd like to allow to news to target specific groups of users, e.g.
// Role = 'Manager'; Premium is true or Age >= 65; etc