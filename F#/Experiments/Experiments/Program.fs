// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
module Map

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    let a = System.Console.ReadLine
    let b = a()
    printfn "%s" b
    let c = a()
    let d = [1,2,3,4]
    let e = d.map
    0 // return an integer exit code
