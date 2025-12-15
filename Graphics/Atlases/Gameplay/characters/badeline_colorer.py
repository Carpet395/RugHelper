import os
from PIL import Image
import colorsys

# Convert hex color to RGB
def hex_to_rgb(hex_color):
    hex_color = hex_color.lstrip('#')
    return tuple(int(hex_color[i:i+2], 16) for i in (0, 2, 4))

# Function to change hue and recolor image to a specific color
def recolor_image(image, target_color):
    img = image.convert('RGBA')
    arr = img.load()
    
    target_h, target_l, target_s = colorsys.rgb_to_hls(target_color[0]/255.0, target_color[1]/255.0, target_color[2]/255.0)
    
    for y in range(img.size[1]):
        for x in range(img.size[0]):
            r, g, b, a = arr[x, y]
            h, l, s = colorsys.rgb_to_hls(r/255.0, g/255.0, b/255.0)
            # Keep the luminance and saturation, but adjust the hue to match target color
            new_r, new_g, new_b = colorsys.hls_to_rgb(target_h, l, s)
            arr[x, y] = (int(new_r * 255), int(new_g * 255), int(new_b * 255), a)
    
    return img

# Avoid file names that contain these strings
skip_list = ["hair"]  # Add strings for files to avoid
allow_list = ['.png', '.jpg', '.jpeg']  # Add allowed file extensions

# Function to remove leading and trailing double quotes from a string
def clean_path(path):
    return path.strip('"')

# Main function to loop through a folder or change a single image
def recolor_images_in_folder(folder_path, hex_color):
    target_color = hex_to_rgb(hex_color)
    
    for filename in os.listdir(folder_path):
        # Check if file should be skipped
        if any(skip in filename for skip in skip_list):
            print(f"Skipping {filename}")
            continue
        
        # Check if the file extension is in the allow list
        if not any(filename.endswith(ext) for ext in allow_list):
            print(f"Skipping {filename}: not in allow list.")
            continue
        
        file_path = os.path.join(folder_path, filename)

        # Open image and recolor
        try:
            with Image.open(file_path) as img:
                img = recolor_image(img, target_color)
                img.save(file_path)  # Overwrite the original image
                print(f"Overwritten {filename}")
        except Exception as e:
            print(f"Error processing {filename}: {e}")

def recolor_single_image(file_path, hex_color):
    target_color = hex_to_rgb(hex_color)
    try:
        with Image.open(file_path) as img:
            img = recolor_image(img, target_color)
            img.save(file_path)  # Overwrite the original image
            print(f"Overwritten {file_path}")
    except Exception as e:
        print(f"Error processing {file_path}: {e}")

# Example usage
mode = input("Do you want to recolor a single image (1) or all images in a folder (2)? Enter 1 or 2:\n")
if mode == '1':
    image_path = clean_path(input("Enter the path of the image:\n"))  # Specify the image path
    hex_color = input("Enter target hex color (e.g., #ff5733):\n")  # Hex color input
    recolor_single_image(image_path, hex_color)
elif mode == '2':
    folder = clean_path(input("Folder to change:\n"))  # Specify your folder path
    hex_color = input("Enter target hex color (e.g., #ff5733):\n")  # Hex color input
    recolor_images_in_folder(folder, hex_color)
else:
    print("Invalid option selected.")
