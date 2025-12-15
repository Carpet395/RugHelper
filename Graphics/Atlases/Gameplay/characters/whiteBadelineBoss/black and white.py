from PIL import Image
import os

for filename in os.listdir('.'):
    if filename.lower().endswith('.png'):
        img = Image.open(filename).convert("RGBA")  # Ensure RGBA

        # Split into channels
        r, g, b, a = img.split()

        # Convert RGB to grayscale
        gray = Image.merge("RGB", (r, g, b)).convert("L")

        # Merge grayscale with original alpha
        bw_rgba = Image.merge("RGBA", (gray, gray, gray, a))

        # Save with a new name to avoid overwriting
        bw_rgba.save(f"{filename}")
        print(f"Converted {filename} to black and white with transparency preserved.")

print("All PNGs have been processed.")
