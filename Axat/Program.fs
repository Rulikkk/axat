// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open System;
open System.IO;

let stream = new StreamWriter("result.txt")

// Перечислить файлы в папке
let filesInDir dir = 
    seq { yield! Directory.EnumerateFiles dir }

// Перечислить строки в файле
let enumerateLines (file : string) = 
    seq {
        use reader = new StreamReader(file)
        while not reader.EndOfStream do
            yield reader.ReadLine()
    }

// Обработать файлы
let processFile file =
    // Перечислить строки
    let lines = enumerateLines file

    let a = 
        lines 
        |> Seq.skipWhile (fun s -> s <> "+TROP/SOLUTION")
        |> Seq.skip 2
        |> Seq.takeWhile (fun s -> s <> "-TROP/SOLUTION")

    // Записать результат в файл
    for line in a do
        stream.WriteLine(line)

[<EntryPoint>]
let main argv = 
    // Получить папку с файлами
    let files = filesInDir argv.[0]
    // Обработать все файлы
    Seq.iter processFile files
    // Сохранить результаты
    stream.Flush()
    0 // return an integer exit codea