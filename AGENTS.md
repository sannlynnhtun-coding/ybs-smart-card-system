# Agent Rules

- Each API endpoint must have its own request model and its own response model.
- The request model must include all endpoint inputs from route, query string, and body.
- API endpoints must return response models, not EF Core entity models directly.
- Listing endpoints and listing pages must use pagination and order results descending by default.
- Do not share request or response models across API endpoints.
- Shared models are allowed only when their properties match the EF Core entity exactly.
- If an API contract needs different properties than the EF Core entity, create an endpoint-specific request or response model.
