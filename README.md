# Nano Banana Render For AutoCAD

这是一个基于您现有Blender插件功能开发的AutoCAD插件，使用Google Gemini 2.5 Flash进行AI驱动的图像生成和分析。

## 功能特性

### 核心功能
- **视口捕获**: 自动捕获当前AutoCAD视口
- **AI分析**: 使用Google Gemini对设计进行专业分析
- **图像生成**: 基于捕获的视口和提示词生成新图像
- **多种渲染风格**: 建筑、写实、艺术、技术、概念等多种风格

### 主要特点
- **智能提示词系统**: 支持多种预设风格和光照效果
- **灵活的相机角度**: 鸟瞰、仰视、特写、广角等多种视角
- **高级参数控制**: Seed、Steps、引导比例等精细控制
- **自动化工作流**: 一键完成从捕获到分析的全过程

## 安装说明

### 系统要求
- AutoCAD 2020 或更高版本
- .NET Framework 4.8
- Windows 10/11

### 安装步骤

1. **编译插件**
   ```bash
   # 在Visual Studio中打开NanoBananaRenderer.csproj
   # 确保AutoCAD DLL引用路径正确
   # 编译为Release版本
   ```

2. **安装依赖**
   - 确保安装了Newtonsoft.Json NuGet包
   - 检查AutoCAD API引用是否正确

3. **部署插件**
   ```autocad
   NETLOAD
   # 选择编译好的NanoBananaRenderer.dll文件
   ```

4. **配置API密钥**
   - 从Google AI Studio获取API密钥: https://makersuite.google.com/app/apikey
   - 运行`NANOBANANA`命令打开设置界面
   - 输入并测试API密钥

## 使用方法

### 基本使用

1. **打开插件界面**
   ```autocad
   NANOBANANA
   ```

2. **配置设置**
   - 在"Settings"标签页输入Gemini API密钥
   - 设置输出目录和其他偏好

3. **开始渲染**
   - 在"AI Renderer"标签页输入提示词
   - 选择风格、光照和相机角度
   - 点击"Start AI Rendering"

### 快速命令

```autocad
NANOBANANA          # 打开主界面
NANOBANANASETTINGS  # 打开设置对话框
NANOBANANARENDER    # 快速渲染当前视口
NANOBANANAHELP      # 显示帮助信息
```

## 技术架构

### 核心组件

1. **Commands.cs**: AutoCAD命令接口
   - 实现IExtensionApplication接口
   - 定义NANOBANANA系列命令
   - 处理AutoCAD集成逻辑

2. **GeminiApiClient.cs**: AI API集成
   - 异步API调用处理
   - 图像上传和分析
   - 错误处理和重试机制

3. **RendererSettings.cs**: 设置管理
   - 风格和参数预设
   - JSON配置持久化
   - 用户偏好管理

4. **RendererForm.cs**: 用户界面
   - 三标签页设计(Renderer/Settings/Advanced)
   - 实时参数调整
   - 结果显示和保存

### API集成

- **Google Gemini 2.5 Flash**: 图像分析和生成
- **AutoCAD .NET API**: 视口操作和文档管理
- **Windows Forms**: 用户界面框架
- **Newtonsoft.Json**: API数据序列化

## 开发说明

### Windows开发环境

1. **必需软件**
   - Visual Studio 2019/2022
   - AutoCAD 2020+ (用于API引用)
   - .NET Framework 4.8 SDK

2. **项目配置**
   ```xml
   <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
   <RootNamespace>NanoBananaRenderForAutoCAD</RootNamespace>
   ```

### macOS开发环境

1. **跨平台开发**
   - 使用.NET 8.0进行核心功能开发
   - MockAutoCAD类模拟AutoCAD API
   - 独立测试和调试功能

2. **构建命令**
   ```bash
   chmod +x build.sh
   ./build.sh
   ```

## 故障排除

### 常见问题

1. **插件加载失败**
   - 检查.NET Framework版本
   - 确认AutoCAD版本兼容性
   - 验证DLL依赖路径

2. **API调用失败**
   - 验证网络连接
   - 检查API密钥有效性
   - 查看错误日志详情

3. **界面显示问题**
   - 确认Windows Forms支持
   - 检查分辨率和DPI设置
   - 重启AutoCAD重新加载

## 版本历史

### v1.3.9 (当前版本)
- 基于Blender插件重新设计AutoCAD版本
- 完整的Google Gemini 2.5 Flash集成
- 跨平台开发支持
- 现代化UI设计

### 计划功能
- [ ] 更多AI模型支持
- [ ] 批量处理模式
- [ ] 插件更新检查
- [ ] 云端设置同步
- [ ] 更多输出格式支持

## 许可证

本项目基于MIT许可证开源，详见LICENSE文件。

## 支持

如有问题或建议，请通过以下方式联系：
- GitHub Issues: https://github.com/why198546/NanoBananaRenderForAutoCAD/issues
- 技术文档: 查看BUILD_GUIDE.md

---

**注意**: 本插件需要有效的Google Gemini API密钥才能正常工作。请确保从Google AI Studio获取并配置API密钥。