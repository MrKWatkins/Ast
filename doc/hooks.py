import logging

try:
    from global_state import global_state
except ModuleNotFoundError:
    from doc.global_state import global_state

logger = logging.getLogger("mkdocs")


def on_nav(nav, config, files):
    global_state["nav_tree"] = nav.items
    logger.log(logging.INFO, "Captured navigation tree.")
