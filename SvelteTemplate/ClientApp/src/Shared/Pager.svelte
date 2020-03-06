<script>
  export let page;
</script>

<nav aria-label="...">
  <ul class="pagination">
    <li class="page-item active">
      <a class="page-link" href="/" on:click|preventDefault={x => page(1)}>1</a>
    </li>
    <li class="page-item active">
      <a class="page-link" href="/" on:click|preventDefault={x => page(2)}>2</a>
    </li>
    <li class="page-item active">
      <a class="page-link" href="/" on:click|preventDefault={x => page(3)}>3</a>
    </li>
    <li class="page-item active">
      <a class="page-link" href="/" on:click|preventDefault={x => page(4)}>4</a>
    </li>
  </ul>
</nav>
