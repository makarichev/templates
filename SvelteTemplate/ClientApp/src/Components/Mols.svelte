<script context="module">
  import { writable } from "svelte/store";
  import { stringify } from "query-string";
    import Pager from "../Shared/Pager.svelte";

  export const mols = writable([]);
  export const molsFilter = writable([]);

  molsFilter.subscribe(async x => {
    let q = await fetch(`api/mols?${stringify(x)}`, {});
    mols.set(await q.json());
  });
</script>

<script>
  import Layout from "../Shared/Layout.svelte";
  import { filter } from "../Shared/Store";
  import { Pagination, PaginationItem, PaginationLink } from "sveltestrap";

  let pager = { page: 4, linit: 10, totals: 2000 };

function filterApply(search) {
    molsFilter.set({search, page:1});
}

</script>

<Layout bind:filterApply search={$molsFilter.search}>
  <div class="container">
    <h5>Пользователи.</h5>

    <table class="table table-sm">
      {#each $mols as item}
        <tr>
          <td>{item.MOL_ID}</td>
          <td>{item.NAME}</td>
          <td>{item.NAME_LOGON}</td>
          <td>{item.EMAIL}</td>
          <td>{item.DEPT_NAME}</td>
        </tr>
      {/each}
      <footer class="pt-4">
ююю
        <Pager page={x => $molsFilter.page = x}></Pager>

      </footer>
    </table>

  </div>

  <div slot="filter"></div>

</Layout>
