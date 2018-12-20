module ShareModels

open DTOs

// The shared interface representing your client-server interaction
type IJobStore = {
    storeJob : Job -> Async<unit> 
}

let jobStore : IJobStore = {
    storeJob = fun job -> async { return () }
} 