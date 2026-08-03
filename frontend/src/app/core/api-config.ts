// Points at the backend's plain-HTTP dev endpoint (see KinGraph.Web/Properties/launchSettings.json)
// on purpose: UseHttpsRedirection() on the backend drops the Authorization header on redirect,
// so the frontend avoids triggering that redirect entirely rather than working around it.
// If running the backend through the Aspire AppHost instead of `dotnet run` directly, update
// this to whatever HTTP endpoint Aspire assigns to KinGraph.Web.
export const API_BASE_URL = 'http://localhost:5234';
