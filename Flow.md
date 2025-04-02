# Git Flow Package Development and Release

## **Branching Strategy**

| Branch      | Purpose                     | Versioning                       | Deployment                   |
| ----------- | --------------------------- | -------------------------------- | ---------------------------- |
| `main`      | Stable production code      | `1.0.0`, `1.1.0`                 | **Public NuGet (NuGet.org)** |
| `develop`   | Active development          | `1.1.0-beta.1`, `1.1.0-beta.2`   | **Internal NuGet**           |
| `feature/*` | New feature development     | `1.2.0-alpha.1`, `1.2.0-alpha.2` | No deployment                |
| `release/*` | Pre-release testing         | `1.1.0-rc.1`, `1.1.0-rc.2`       | **Staging NuGet**            |
| `hotfix/*`  | Urgent fixes for production | `1.0.1`, `1.0.2`                 | **Public NuGet**             |

---

## **Git Workflow & Development Process**

### **1️⃣ Start New Feature**

- Create a feature branch from `develop`:
  ```sh
  git checkout -b feature/new-authentication develop
  ```
- Implement changes and commit:
  ```sh
  git add .
  git commit -m "Added OAuth2 authentication"
  ```
- Push the branch and create a **Pull Request (PR)** to `develop`.

### **2️⃣ Merge Feature into Develop**

- Once reviewed, merge the feature into `develop`:
  ```sh
  git checkout develop
  git merge feature/new-authentication
  git push origin develop
  ```
- This triggers a **CI build** and publishes an **alpha/beta NuGet package**.

### **3️⃣ Preparing for a Release**

- Create a `release` branch from `develop`:
  ```sh
  git checkout -b release/1.1.0 develop
  git push origin release/1.1.0
  ```
- Only **bug fixes** are allowed here.
- A **Release Candidate (**\`\`**) NuGet package** is created.

### **4️⃣ Finalizing the Release**

- Merge `release/1.1.0` into `main`:
  ```sh
  git checkout main
  git merge release/1.1.0
  git tag v1.1.0
  git push origin main --tags
  ```
- Merge `release/1.1.0` back into `develop`:
  ```sh
  git checkout develop
  git merge release/1.1.0
  git push origin develop
  ```
- **NuGet Package** `1.1.0` is published to NuGet.org.

### **5️⃣ Hotfixes for Critical Bugs**

- Create a `hotfix` branch from `main`:
  ```sh
  git checkout -b hotfix/1.1.1 main
  ```
- After fixing the bug:
  ```sh
  git add .
  git commit -m "Fixed API authentication issue"
  git checkout main
  git merge hotfix/1.1.1
  git tag v1.1.1
  git push origin main --tags
  ```
- \*\*NuGet Package \*\*\`\` is released.
- Merge hotfix back into `develop`:
  ```sh
  git checkout develop
  git merge hotfix/1.1.1
  git push origin develop
  ```

---

## **GitVersion Configuration (Auto Versioning)**

Add this to `.azure/pipelines/GitVersion.yml`:

```yaml
mode: ContinuousDeployment

branches:
  main:
    tag: ''                 # No pre-release tag for stable versions.
    is-release-branch: true # Merging here creates a stable version.
    increment: Patch        # Increment patch version for each merge.

  develop:
    tag: beta              # Pre-release versions (e.g., 1.0.0-beta.1).
    increment: Minor       # Increase minor version for new features.

  release:
    tag: rc               # Release candidate versions (e.g., 1.0.0-rc.1).
    is-release-branch: true
    increment: Patch

  feature:
    tag: alpha             # Feature branches get an alpha version.
    increment: Minor       # Each feature branch increments minor.

  hotfix:
    tag: ''                # No special tag, just an increment.
    increment: Patch       # Hotfixes increase patch version.

  support:
    tag: support           # Special branch (if needed).
    increment: Patch

ignore:
  sha: []                  # Can ignore specific commits if needed.

merge-message-formats: {}   # No special rules for merge commits.
```

---

### **2️⃣ NuGet Deployment Strategy**

| Branch      | NuGet Version  | Deployment Target        |
| ----------- | -------------- | ------------------------ |
| `develop`   | `1.1.0-beta.1` | Internal NuGet feed      |
| `release/*` | `1.1.0-rc.1`   | Staging NuGet feed       |
| `main`      | `1.1.0`        | Public NuGet (NuGet.org) |

## **Summary**

1️⃣ **Develop Features**  → Feature branches → `develop` → **beta NuGet package**\
2️⃣ **Prepare for Release**  → `release/*` → **rc NuGet package**\
3️⃣ **Final Release**  → `main` → **Final NuGet package**\
4️⃣ **Bug Fixes**  → `hotfix/*` → **Patch NuGet package**

