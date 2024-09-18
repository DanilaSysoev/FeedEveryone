from os import walk, path

lines_count = 0
test_lines_count = 0

for root, dirs, files in walk("./"):
    for file in files:
        if file.endswith(".cs"):
            with open(path.join(root, file), "r") as f:
                text = f.read()
                if '[Test]' in text:
                    test_lines_count += len(text.splitlines())
                else:
                    lines_count += len(text.splitlines())

print(f'Tests: {test_lines_count}\nCode: {lines_count}')
