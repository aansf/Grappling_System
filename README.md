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

🧪 Quality Analysis Responsibilities

As a Quality Analyst, I focused on ensuring the grappling system behaved consistently, responded accurately to player actions, and delivered a smooth gameplay feel. Key QA contributions include:

---

✔️ Functional Testing

Tested grapple detection accuracy across different distances, angles, and surfaces.

Verified smooth transitions between pull, swing, and release states.

Ensured rope rendering updated correctly during rapid movement and camera changes.

---

✔️ Physics & Stability Testing

Checked for jitter, unwanted force spikes, or unnatural motion in custom physics calculations.

Validated Rigidbody behavior under extreme values (high force, low damping, sudden changes).

Identified inconsistencies in swing radius, damping, and pull strength under different frame rates.

---

✔️ Bug Identification & Debugging Support

Logged issues related to raycast failures, incorrect collision responses, or misaligned grapple points.

Debugged common problems like rope snapping, infinite swinging, and force accumulation.

Assisted in refining equations for smoother, more natural movement.

---

✔️ Gameplay Feel & Balance Testing

Tested tuning values (pullForce, swingStiffness, damping, steerForce) to find balanced, fun settings.

Provided feedback to improve responsiveness, air control, and momentum retention.

Evaluated player flow from grappling → swinging → landing for overall enjoyment.

---

✔️ Edge Case Testing

Tested behavior when trying to grapple from ground, during jumps, or near obstacles.

Checked for unintended grapples through walls, on invalid objects, or when switching targets quickly.

Ensured system handled maximum grapple distance and cancellation cleanly.

---
