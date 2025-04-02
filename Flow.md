# GitLab Release Branch Flow

## Overview
This repository follows the **GitLab Release Branch Flow**, ensuring structured and reliable deployments.

### **Flow Summary:**
- A **release branch** is created from `main` for every new production release.
- The release branch generates a **production-ready build** and deploys it to **QA** and **Production** environments.
- Once deployed to **Production**, the changes from the release branch are merged back into `main`.
- Any **new features or changes** are based on the current release branch and merged back into it.
- For each **new release**, a fresh **release branch** is created from `main`.

---
## **Branching Strategy**

### **1️⃣ Creating a New Release Branch**
```sh
# Create a new release branch from main
 git checkout main
 git pull origin main
 git checkout -b release/vX.Y
 git push origin release/vX.Y
```

### **2️⃣ Feature Development**
- New features are branched from the **current release branch**:
```sh
# Create a feature branch from the release branch
git checkout release/vX.Y
git checkout -b feature/awesome-feature
git push origin feature/awesome-feature
```
- Once completed, the feature branch is merged back into the release branch:
```sh
git checkout release/vX.Y
git merge feature/awesome-feature
git push origin release/vX.Y
```

### **3️⃣ Deploying to QA**
- The release branch is **deployed to QA** for testing using CI/CD.
- Fixes found in QA are applied directly to the **release branch**.

### **4️⃣ Deploying to Production**
- Once QA is approved, the release branch is deployed to production.
- After a successful deployment, the release branch is merged into `main`:
```sh
git checkout main
git merge release/vX.Y
git push origin main
```

### **5️⃣ Preparing for the Next Release**
- After deployment, a **new release branch** is created from `main` for the next version.
```sh
git checkout main
git pull origin main
git checkout -b release/vX.Z
git push origin release/vX.Z
```

---
## **CI/CD Pipeline Integration**

### **Pipeline Stages:**
1. **Build** - Compiles and packages the application.
2. **Deploy to QA** - Deploys the release branch to a **staging/QA environment**.
3. **Deploy to Production** - Once validated in QA, the build is deployed to production.
4. **Merge to Main** - After production deployment, changes are merged back into `main`.

---
## **Example GitLab CI/CD Configuration (`.gitlab-ci.yml`)**
```yaml
stages:
  - build
  - deploy_qa
  - deploy_prod

build:
  stage: build
  script:
    - echo "Building the application..."
    - ./build.sh
  artifacts:
    paths:
      - build/

qa_deploy:
  stage: deploy_qa
  script:
    - echo "Deploying to QA..."
  only:
    - /^release\/.*$/

prod_deploy:
  stage: deploy_prod
  script:
    - echo "Deploying to Production..."
  only:
    - /^release\/.*$/
    - main
```

---
## **Best Practices**
✔ Keep `main` always production-ready.
✔ Use **feature branches** for new developments.
✔ Deploy to **QA** before Production.
✔ Merge the **release branch** back to `main` after successful deployment.
✔ Create a **new release branch** for every major release.
