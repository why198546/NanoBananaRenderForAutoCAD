using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using NanoBananaRenderForAutoCAD.UI;
using NanoBananaRenderForAutoCAD.Core;

namespace NanoBananaRenderForAutoCAD
{
    public class Commands : IExtensionApplication
    {
        public void Initialize()
        {
            // Plugin initialization
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc != null)
            {
                Editor ed = doc.Editor;
                ed.WriteMessage("\nNano Banana AI Renderer loaded successfully!");
                ed.WriteMessage("\nType 'NANOBANANA' to start AI rendering.");
            }
        }

        public void Terminate()
        {
            // Plugin cleanup
        }

        [CommandMethod("NANOBANANA")]
        public void ShowNanoBananaRenderer()
        {
            try
            {
                Document doc = Application.DocumentManager.MdiActiveDocument;
                Editor ed = doc.Editor;

                // Show the main renderer form
                RendererForm form = new RendererForm();
                Application.ShowModalDialog(form);
            }
            catch (System.Exception ex)
            {
                Document doc = Application.DocumentManager.MdiActiveDocument;
                if (doc != null)
                {
                    Editor ed = doc.Editor;
                    ed.WriteMessage($"\nError: {ex.Message}");
                }
            }
        }

        [CommandMethod("NANOBANANASETTINGS")]
        public void ShowSettings()
        {
            try
            {
                Document doc = Application.DocumentManager.MdiActiveDocument;
                Editor ed = doc.Editor;

                // Show settings dialog
                RendererForm form = new RendererForm();
                form.ShowSettingsTab();
                Application.ShowModalDialog(form);
            }
            catch (System.Exception ex)
            {
                Document doc = Application.DocumentManager.MdiActiveDocument;
                if (doc != null)
                {
                    Editor ed = doc.Editor;
                    ed.WriteMessage($"\nError: {ex.Message}");
                }
            }
        }

        [CommandMethod("NANOBANANARENDER")]
        public void QuickRender()
        {
            try
            {
                Document doc = Application.DocumentManager.MdiActiveDocument;
                Editor ed = doc.Editor;

                // Check if API key is configured
                RendererSettings settings = RendererSettings.Load();
                if (string.IsNullOrEmpty(settings.ApiKey))
                {
                    ed.WriteMessage("\nPlease configure your Gemini API key first using NANOBANANA command.");
                    return;
                }

                ed.WriteMessage("\nStarting AI rendering...");

                // Capture viewport
                ViewportCapture capture = new ViewportCapture();
                string imagePath = capture.CaptureCurrentViewport();

                if (!string.IsNullOrEmpty(imagePath))
                {
                    ed.WriteMessage($"\nViewport captured: {imagePath}");
                    
                    // Start AI processing in background
                    GeminiApiClient client = new GeminiApiClient(settings.ApiKey);
                    client.ProcessImageAsync(imagePath, settings.ImagePrompt, (result) =>
                    {
                        if (result.Success)
                        {
                            ed.WriteMessage($"\nAI rendering completed: {result.OutputPath}");
                        }
                        else
                        {
                            ed.WriteMessage($"\nAI rendering failed: {result.ErrorMessage}");
                        }
                    });
                }
                else
                {
                    ed.WriteMessage("\nFailed to capture viewport.");
                }
            }
            catch (System.Exception ex)
            {
                Document doc = Application.DocumentManager.MdiActiveDocument;
                if (doc != null)
                {
                    Editor ed = doc.Editor;
                    ed.WriteMessage($"\nError: {ex.Message}");
                }
            }
        }

        [CommandMethod("NANOBANANAHELP")]
        public void ShowHelp()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc != null)
            {
                Editor ed = doc.Editor;
                ed.WriteMessage("\n=== Nano Banana AI Renderer Help ===");
                ed.WriteMessage("\nCommands:");
                ed.WriteMessage("\n  NANOBANANA - Open main renderer interface");
                ed.WriteMessage("\n  NANOBANANASETTINGS - Open settings dialog");
                ed.WriteMessage("\n  NANOBANANARENDER - Quick render current viewport");
                ed.WriteMessage("\n  NANOBANANAHELP - Show this help");
                ed.WriteMessage("\n");
                ed.WriteMessage("\nFeatures:");
                ed.WriteMessage("\n  • AI-powered image generation using Google Gemini 2.5 Flash");
                ed.WriteMessage("\n  • Viewport capture and analysis");
                ed.WriteMessage("\n  • Multiple rendering styles and prompts");
                ed.WriteMessage("\n  • Professional rendering analysis and suggestions");
                ed.WriteMessage("\n");
                ed.WriteMessage("\nSetup:");
                ed.WriteMessage("\n  1. Get API key from Google AI Studio: https://makersuite.google.com/app/apikey");
                ed.WriteMessage("\n  2. Run NANOBANANA command to configure");
                ed.WriteMessage("\n  3. Enter your API key and customize settings");
                ed.WriteMessage("\n  4. Start rendering!");
            }
        }
    }
}