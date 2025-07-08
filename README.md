🪝 `Grappling Hook Physics System`

### 🕸️ Overview

A physics-based grappling hook system in Unity that replicates Spider-Man–like swinging, pulling, and air control using custom force equations — **no SpringJoint used**.

---

### 🎮 Features

* **Custom Grapple Mechanics**:

  * Raycast-based grapple detection
  * Rope pulls player toward grapple point
  * Smooth transition from **pulling to swinging**
* **Physics System**:

  * Force-based movement using Rigidbody
  * Custom swing dynamics (radial + tangential damping)
* **Air Control**:

  * Enhanced directional control while swinging
* **Rope Rendering**:

  * Dynamic `LineRenderer` that visually connects player and grapple point

---

### 🎯 Grapple Settings

| Setting              | Description                            |
| -------------------- | -------------------------------------- |
| `maxGrappleDistance` | Max distance for raycast connection    |
| `pullForce`          | Force applied when pulling             |
| `swingStiffness`     | Tightness of swing arc                 |
| `damping`            | General motion damping                 |
| `steerForce`         | Directional control during swing       |
| `stopDistance`       | Automatically stops grapple near point |
| `groundCheck`        | Ensures grapple only when airborne     |

---

### 🧠 System Flow

1. **Fire grapple** using camera direction and raycast
2. **Attach rope** and apply pull force
3. If close enough, transition to **swing physics**
4. **Apply forces** dynamically based on position and direction
5. **Release** when close or on input

---

### 🔧 Code Highlights

```csharp
// Force calculation (simplified)
Vector3 direction = grapplePoint - transform.position;
Vector3 pull = direction.normalized * pullForce;
Vector3 swingForce = CalculateSwingForces(...);

rb.AddForce(pull + swingForce, ForceMode.Acceleration);
```
---

### 🚀 How to Use

* Assign `RealisticGrapple.cs` to your player GameObject
* Setup:

  * `Rigidbody`
  * `LineRenderer`
  * `HookOrigin`, `Camera`, and `GroundCheck`
* Customize values in Inspector

---

### 🛠️ Future Additions

* Swing boost via jump input
* Obstacle detection while swinging
* Cooldown and energy system
* Enemies and combat while swinging

---
