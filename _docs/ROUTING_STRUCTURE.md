# EventEase - Routing Structure

## Overview

The EventEase application implements a comprehensive routing system with seamless navigation between pages.

## Route Map

### 1. Home Page (`/`)

- **Component:** `Home.razor`
- **Purpose:** Landing page with hero section and features
- **Navigation Options:**
  - "Browse Events" button → navigates to `/events`

### 2. Events List Page (`/events`)

- **Component:** `Events.razor`
- **Purpose:** Display all available events with filtering capability
- **Features:**
  - Category filter with two-way data binding
  - Grid layout of EventCard components
  - Dynamic event loading from EventService
- **Navigation Options:**
  - "View Details" button on each card → navigates to `/event/{id}`

### 3. Event Details Page (`/event/{EventId:int}`)

- **Component:** `EventDetails.razor`
- **Purpose:** Show detailed information about a specific event
- **Route Parameter:** `EventId` (integer)
- **Features:**
  - Full event information display
  - Category badge
  - Date, location, capacity, and description
- **Navigation Options:**
  - "Register for Event" button → navigates to `/event/{id}/register`
  - "Back to Events" button → navigates to `/events`

### 4. Event Registration Page (`/event/{EventId:int}/register`)

- **Component:** `EventRegistration.razor`
- **Purpose:** Allow users to register for events
- **Route Parameter:** `EventId` (integer)
- **Features:**
  - Registration form with validation
  - Two-way data binding for form fields
  - Form validation
  - Success confirmation message
- **Navigation Options:**
  - "Complete Registration" button → shows success message
  - "Cancel" button → navigates to `/event/{id}`
  - "Browse More Events" button (after success) → navigates to `/events`
  - "View Event Details" button (after success) → navigates to `/event/{id}`

## Navigation Menu

The main navigation menu (NavMenu.razor) includes:

- Home
- Events
- Counter (demo page)
- Weather (demo page)

## Best Practices Implemented

### 1. Route Constraints

- Used integer constraint for EventId: `{EventId:int}`
- Ensures type safety and prevents invalid route parameters

### 2. NavigationManager Injection

- Injected in all components requiring navigation
- Provides programmatic navigation capability

### 3. NavLink Component

- Used in NavMenu for automatic active state styling
- Improves user experience with visual feedback

### 4. Event Callbacks

- EventCard component uses EventCallback for click handling
- Maintains proper parent-child component communication

### 5. Proper Back Navigation

- Consistent "Back" buttons on detail and registration pages
- Users can easily return to previous contexts

### 6. Route Parameters

- Dynamic routing with EventId parameter
- Enables deep linking to specific events

## Data Flow

```
Home (/)
  → Events (/events)
      → Event Details (/event/{id})
          → Registration (/event/{id}/register)
              → Success → Events or Event Details
```

## Component Dependencies

- **EventService:** Provides mock data and CRUD operations
- **Event Model:** Data structure for event objects
- **EventCard Component:** Reusable card component for event display
- **NavigationManager:** Built-in Blazor service for routing

## Form Binding Examples

### Two-Way Binding in Events.razor

```razor
<select @bind="selectedCategory" @bind:after="FilterEvents">
```

### Form Input Binding in EventRegistration.razor

```razor
<input type="text" @bind="registrationData.FullName" />
<input type="email" @bind="registrationData.Email" />
<input type="checkbox" @bind="registrationData.AcceptTerms" />
```

## Testing the Routes

1. Start application: `dotnet run --project EventEase.csproj`
2. Navigate to: `https://localhost:{port}/`
3. Test all navigation flows:
   - Home → Events → Event Details → Registration
   - Use back buttons to verify return navigation
   - Test filter functionality on Events page
   - Submit registration form
