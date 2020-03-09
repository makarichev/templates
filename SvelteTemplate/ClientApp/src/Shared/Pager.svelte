<script>
  import { createEventDispatcher } from "svelte";
  const dispatch = createEventDispatcher();

  export let limit = 1;
  export let total = 0;
  export let offset = 1;
  export let pages = [];

  let onPaged = p => dispatch("paged", p);

  $: length = Math.ceil(total / limit);

  $: {
    if (length)
      pages = Array(length)
        .fill(null)
        .map((x, i) => i + 1);
  }

  $: current = 1 + offset / limit;
  $: isCurrent = page => offset === page * limit - limit;

  $: showPager = total > limit;
</script>

{#if showPager}
  <nav aria-label="...">
    <ul class="pagination">
      {#each pages as item}
        {#if item === 1 || item === length || (item >= current - 2 && item <= current + 2)}
          <li class="page-item" class:active={current == item}>
            <a
              class="page-link"
              href="/"
              on:click|preventDefault={onPaged(item)}>
              {item}
            </a>
          </li>
        {:else if item === 2 || item === length - 1}
          <li class="page-item disabled">
            <a class="page-link" href="/">...</a>
          </li>
        {/if}
      {/each}
    </ul>
  </nav>
{/if}
