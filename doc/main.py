import logging

try:
    from global_state import global_state
except ModuleNotFoundError:
    from doc.global_state import global_state

logger = logging.getLogger("mkdocs")


def define_env(env):
    @env.macro
    def global_nav():
        logger.log(logging.INFO, "Rendering global navigation tree...")
        nav_tree = global_state.get("nav_tree")
        if not nav_tree:
            logger.log(logging.ERROR, "No navigation tree in global state.")
            return "Unable to render navigation tree."

        result = "\n".join(render(env.conf, nav_tree))
        logger.log(logging.INFO, result)
        return result


def render(conf, items, indent=0):
    lines = []
    for item in items:
        if item.is_section:
            lines.append("    " * indent + f"- {item.title}")

            if item.children:
                lines.extend(render(conf, item.children, indent + 1))
        elif item.is_page:
            if not item.is_homepage:
                item.read_source(conf)
                lines.append("    " * indent + f"- [{item.title}]({item.file.src_uri})")
        else:
            raise Exception(f"Unexpected item type: {type(item)}")

    return lines
