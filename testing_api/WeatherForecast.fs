namespace testing_api

open System

type WeatherForecast =
    { Date: DateTime
      TemperatureC: int
      Summary: string } // this 

    member this.TemperatureF =
        32.0 + (float this.TemperatureC / 0.5556)
