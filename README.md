### Role-Based Authorization

| Action | User | Admin |
|---|---|---|
| Register / Login | ✅ | ✅ |
| View Projects & Tasks | ✅ | ✅ |
| Create Projects & Tasks | ✅ | ✅ |
| Update Projects & Tasks | ✅ | ✅ |
| Delete Projects & Tasks | ❌ | ✅ |

---

## 🗺️ API Endpoints Summary

### Authentication Module

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/Auth/register` | Register a new identity |
| POST | `/api/Auth/login` | Authenticate and return JWT token |

### Projects Module

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Projects` | Fetch all projects (paginated) | User / Admin |
| GET | `/api/Projects/{id}` | Fetch a specific project by Guid | User / Admin |
| POST | `/api/Projects` | Create a new project | User / Admin |
| PUT | `/api/Projects/{id}` | Update project metadata | User / Admin |
| DELETE | `/api/Projects/{id}` | Delete a project | **Admin Only** |

### Tasks Module

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Tasks` | Fetch all tasks | User / Admin |
| GET | `/api/Tasks/project/{projectId}` | Fetch all tasks for a specific project | User / Admin |
| POST | `/api/Tasks` | Create a new task (with DueDate & Priority validation) | User / Admin |
| PUT | `/api/Tasks/{id}/status` | Update task processing status | User / Admin |
| DELETE | `/api/Tasks/{id}` | Remove a task | **Admin Only** |

---

## 🧪 Running Tests

```bash
cd tests/TaskManagement.Tests
dotnet test
```
---

## 📦 Enums Reference

**TaskStatus:** `0 = Todo` · `1 = InProgress` · `2 = Done` · `3 = Cancelled`

**TaskPriority:** `0 = Low` · `1 = Medium` · `2 = High` · `3 = Critical`

**UserRole:** `0 = User` · `1 = Admin`
