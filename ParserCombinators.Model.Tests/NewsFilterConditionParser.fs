namespace ParserCombinators.Model.Tests

open ParserCombinators.Model
open Xunit

type ``NewsFilterCondition parser tests`` (output) =
    let shouldBe = shouldBe output
    let shouldFail = shouldFail output
    let parser = NewsFilterExpressionParser.newsFilterParser
    
    [<Theory
    InlineData "AND"
    InlineData "role='Customer' OR"
    InlineData "(role='Customer'"
    InlineData "Age > 0 qwe">]
    member _.``Should fail on malformed conditions`` input =
        parser
        |> parse input
        |> shouldFail
    
    [<Theory; ClassData(typeof<NewsFilterConditions>)>]
    member _.``Should parse conditions`` input expected =
        parser
        |> parse input
        |> shouldBe expected

and NewsFilterConditions () as this =
    inherit TheoryData<string, NewsFilterExpression>()
    do
        this.Add("role='Customer' AND age >= 18",
                NewsFilterExpression.And (
                    NewsFilterExpression.Condition (
                        "Customer" |> RoleCondition.Equals |> NewsCondition.Role),
                    NewsFilterExpression.Condition (
                        18 |> AgeCondition.GreaterOrEqual |> NewsCondition.Age)))
        this.Add("(age >= 18 OR Premium is true) AND role='Customer'",
                NewsFilterExpression.And (
                    NewsFilterExpression.Or (
                        NewsFilterExpression.Condition (
                            18 |> AgeCondition.GreaterOrEqual |> NewsCondition.Age),
                        NewsFilterExpression.Condition (
                            true |> IsPremiumCondition.Is |> NewsCondition.IsPremium)),
                    NewsFilterExpression.Condition (
                        "Customer" |> RoleCondition.Equals |> NewsCondition.Role)))