Repro for
- [hot reload takes too much time · Issue \#47179 · dotnet/sdk](https://github.com/dotnet/sdk/issues/47179)
- [dotnet 9, "dotnet watch" is really bad in Blazor Server · Issue \#45810 · dotnet/sdk](https://github.com/dotnet/sdk/issues/45810)

`dotnet watch` is much slower (10-20 seconds vs ~1 second) to apply small changes to `.razor` files in Blazor, after upgrading from .NET SDK 8.0.407 to 9.x.
Have tried `9.0.202` and `9.0.300-preview.0.25174.3`.

A couple of observations in these template apps:
- `dotnet watch --verbose` produces a very long list of files on every change in net9, no such details shown in net8
- `OnInitializedAsync` for `Home.razor` is run twice in net9, once in net8
- Simulating a slow `OnInitializedAsync` with Task.Delay, delays the hot reload

Observations in our proprietary, large Blazor Server code base with many projects, code and components:
- On net9, hot reload incurs what seems like a full page load. A new circuit is started, services are created and data is initialized. This is slow in our app, but this page load did not happen in net8 hot reload.


![image](https://github.com/user-attachments/assets/5e88dc7e-bc29-441c-bffe-eb4dcc8cbb36)

![image](https://github.com/user-attachments/assets/088547ae-ae77-4e85-a92d-4411c6136305)
