import xml.etree.ElementTree as ET
import os
from PIL import Image

# Parse the XML file
def parse_xml(xml_file):
    tree = ET.parse(xml_file)
    root = tree.getroot()
    return root

# Negate the first frame's hair coordinates from idle animation
def negate_idle_hair(root):
    # Find the <Frames> elements for idle animation and get the first frame's hair coordinates
    idle_frames = [frame for frame in root.findall(".//Frames[@path='idle']")]
    first_frame_hair = idle_frames[0].attrib['hair'].split("|")[0]
    
    first_frame_x, first_frame_y = map(int, first_frame_hair.split(","))
    
    # Update the hair coordinates of each frame in idle animation by negating the first frame's values
    for frames in root.findall(".//Frames[@path='idle']"):
        frames_hair = frames.attrib['hair']
        updated_hair = []
        for coord in frames_hair.split("|"):
            try:
                # Handle valid coordinate pairs
                if ':' in coord:
                    print(f"Skipping invalid coordinate: {coord}")
                    continue  # Skip invalid coordinates
                x, y = map(int, coord.split(","))
                new_x = x - first_frame_x
                new_y = y - first_frame_y
                updated_hair.append(f"{new_x},{new_y}")
            except ValueError:
                print(f"Invalid coordinate format: {coord}")
        frames.attrib['hair'] = "|".join(updated_hair)
    
    return first_frame_x, first_frame_y

# Write color pixel to sprite images
def apply_pixel_to_frame(folder, x, y, color, frame_id):
    # Generate the correct sprite file name based on the frame_id
    sprite_file = os.path.join(folder, f"{frame_id}.png")
    
    # Check if the sprite exists for this frame
    if not os.path.exists(sprite_file):
        print(f"Warning: Sprite file {sprite_file} does not exist.")
        return
    
    # Open the sprite image
    img = Image.open(sprite_file)
    
    # Ensure the image is in RGBA mode to manipulate the pixel
    img = img.convert("RGBA")
    
    # Apply the color pixel
    img.putpixel((x, y), color)
    
    # Save the updated image
    img.save(sprite_file)

# Main function to run the script
def main():
    # Get user input
    xml_file = input("Enter the path to the XML file: ")
    folder = input("Enter the folder where the sprite images are stored: ")
    coord_input = input("Enter the coordinates (x, y): ")
    color_input = input("Enter the hex color for the pixel (e.g., #FF5733): ")

    # Validate the hex color
    if not color_input.startswith("#") or len(color_input) != 7:
        print("Invalid hex color. Please use the format #RRGGBB.")
        return
    color = tuple(int(color_input[i:i+2], 16) for i in (1, 3, 5)) + (255,)  # RGBA

    try:
        x_offset, y_offset = map(int, coord_input.split(","))
    except ValueError:
        print("Invalid coordinates. Please use the format x,y.")
        return

    # Parse XML and process idle animation
    root = parse_xml(xml_file)
    negate_idle_hair(root)

    # Apply pixel color to each frame
    for frames in root.findall(".//Frames"):
        path = frames.attrib['path']

        # Look for the corresponding <anim> or <loop> by id and retrieve its path
        animation = root.find(f".//Anim[@path='{path}']")
        loop = root.find(f".//Loop[@path='{path}']")

        if animation is not None:
            real_path = animation.attrib['id']
        elif loop is not None:
            real_path = loop.attrib['id']
        else:
            print(f"Warning: No matching <anim> or <loop> found for {path}. Skipping...")
            continue

        if 'hair' in frames.attrib:
            hair_coords = frames.attrib['hair']
            # Split hair by "|" to get each frame's coordinates
            coords = hair_coords.split("|")
            for i, coord in enumerate(coords):
                if ":" in coord:  # If there's a ':', split at the ',' and only keep the part before the ':'
                    coord = coord.split(":")[0]  # Keep only the part before the ':'
                if coord != "x" and coord != "":  # Skip if it's 'x' indicating no pixel
                    x, y = map(int, coord.split(","))
                    frame_id = f"{real_path}{str(i).zfill(2)}"  # Generate frame name (e.g., slide00, slide01)
                    x_final = x + x_offset
                    y_final = y + y_offset
                    # Apply the color to the corresponding frame's sprite
                    apply_pixel_to_frame(folder, x_final, y_final, color, frame_id)
    print("Pixel coloring applied successfully.")

if __name__ == "__main__":
    main()
